package com.davisdabols.inventarizacijaspaligs.ui

import androidx.lifecycle.ViewModel
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.AppRepository
import com.davisdabols.inventarizacijaspaligs.data.models.*
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.*
import timber.log.Timber
import java.lang.Exception
import javax.inject.Inject
import kotlin.math.log

@HiltViewModel
class AppViewModel @Inject constructor(
    private val repository: AppRepository
) : ViewModel() {
    private val _loggedInUser = MutableStateFlow<WorkerModel?>(null)
    private val _error = MutableSharedFlow<String>()
    private val _logInStatus = MutableSharedFlow<Boolean>(replay=1)
    private val _warehouses = MutableSharedFlow<List<WarehouseModel>>()
    private val _items = MutableSharedFlow<List<ItemsModel>>()
    val loggedInUser = _loggedInUser.asStateFlow()
    val error = _error.asSharedFlow()
    val logInStatus = _logInStatus.asSharedFlow()
    val warehouses = _warehouses.asSharedFlow()
    val items = _items.asSharedFlow()

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
            val workerModel = repository.getWarehouses(loggedInUser.value!!.AdminID)
            _warehouses.emit(workerModel)
        }
    }

    fun getItems() {
        launchIO {
            val itemsModel = repository.getItems(selectedWarehouse!!.ID)
            _items.emit(itemsModel)
        }
    }

    fun postItems(title: String, description: String) {
        val item = ItemsPostModel(title, description, selectedWarehouse!!.ID, loggedInUser.value!!.AdminID)
        launchIO {
            try {
                repository.postItems(item)
            } catch (e: Exception) {
                _error.emit("Kļūda pievienošanā")
            }
        }
    }

    fun deleteItems() {
        launchIO {
            repository.deleteItems(selectedItem!!.ID)
        }
    }

    fun updateItems(name: String, description: String) {
        val item = ItemsPutModel(name, description)
        launchIO {
            repository.updateItems(selectedItem!!.ID, item)
        }
    }
}
