package com.davisdabols.inventarizacijaspaligs.ui

import androidx.lifecycle.ViewModel
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.AppRepository
import com.davisdabols.inventarizacijaspaligs.data.models.*
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.*
import timber.log.Timber
import java.lang.Exception
import javax.inject.Inject
import kotlin.math.log

sealed class WarehouseState {
    object Loading: WarehouseState()
    object IsEmpty: WarehouseState()
    data class Warehouses(val warehouses: List<WarehouseModel>): WarehouseState()
}

sealed class ItemsState {
    object Loading: ItemsState()
    object IsEmpty: ItemsState()
    data class Items(val items: List<ItemsModel>): ItemsState()
}

@HiltViewModel
class AppViewModel @Inject constructor(
    private val repository: AppRepository
) : ViewModel() {
    private val _loggedInUser = MutableStateFlow<WorkerModel?>(null)
    private val _error = MutableSharedFlow<String>()
    private val _logInStatus = MutableSharedFlow<Boolean>(replay=1)
    private val _items = MutableSharedFlow<List<ItemsModel>>()
    private val _warehouseState = MutableStateFlow<WarehouseState>(WarehouseState.Loading)
    private val _itemsState = MutableStateFlow<ItemsState>(ItemsState.Loading)
    val loggedInUser = _loggedInUser.asStateFlow()
    val error = _error.asSharedFlow()
    val logInStatus = _logInStatus.asSharedFlow()
    val items = _items.asSharedFlow()
    val warehouseState = _warehouseState.asStateFlow()
    val itemsState = _itemsState.asStateFlow()

    var selectedWarehouse: WarehouseModel? = null
    var selectedItem: ItemsModel? = null

    fun checkLoggedIn() {
        launchIO {
            val logInStatus = repository.checkLoggedIn()
            if(logInStatus != null) {
                _logInStatus.emit(true)
            } else {
                _logInStatus.emit(false)
            }
            _loggedInUser.emit(logInStatus)
        }
    }

    fun logIn(email: String, password: String) {
        launchIO {
            try {
                val workerModel = repository.checkLogin(email, password)
                _loggedInUser.emit(workerModel)
            } catch (e: Exception) {
                _error.emit("Lietotājs nav atrasts")
            }
        }
    }

    fun logOut() {
        launchIO {
            repository.logOut()
            _loggedInUser.emit(null)
        }
    }

    fun getWarehouses() {
        launchIO {
            _warehouseState.value = WarehouseState.Loading
            val warehouses = repository.getWarehouses(loggedInUser.value!!.AdminID)
            _warehouseState.value =
                if (warehouses.isEmpty()) {
                    WarehouseState.IsEmpty
                } else {
                    WarehouseState.Warehouses(warehouses)
                }
        }
    }

    fun getItems() {
        launchIO {
            _itemsState.value = ItemsState.Loading
            val items = repository.getItems(selectedWarehouse!!.ID)
            _itemsState.value =
                if (items.isEmpty()) {
                    ItemsState.IsEmpty
                } else {
                    ItemsState.Items(items)
                }
        }
    }

    fun postItems(barcode: String?, title: String, description: String, count: Int, price: Float) {
        val item = ItemsPostModel(barcode, title, description, count, price, selectedWarehouse!!.ID, loggedInUser.value!!.AdminID)
        val log = LogsPostModel(loggedInUser.value!!.Name, loggedInUser.value!!.Surname, loggedInUser.value!!.AdminID, title, 'A')
        launchIO {
            try {
                repository.postItems(item)
                repository.postLogs(log)
            } catch (e: Exception) {
                _error.emit("Kļūda pievienošanā")
            }
        }
    }

    fun deleteItems() {
        val log = LogsPostModel(loggedInUser.value!!.Name, loggedInUser.value!!.Surname, loggedInUser.value!!.AdminID, selectedItem!!.Name, 'D')
        launchIO {
            repository.deleteItems(selectedItem!!.ID)
            repository.postLogs(log)
        }
    }

    fun updateItems(barcode: String?, name: String, description: String?, count: Int, price: Float, warehouseId : String, switchedWarehouses: Char) {
        val item = ItemsPutModel(barcode, name, description, count, price, warehouseId)
        val log = LogsPostModel(loggedInUser.value!!.Name, loggedInUser.value!!.Surname, loggedInUser.value!!.AdminID, name, switchedWarehouses)
        launchIO {
            repository.updateItems(selectedItem!!.ID, item)
            repository.postLogs(log)
        }
    }
}
