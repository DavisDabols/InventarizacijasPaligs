package com.davisdabols.inventarizacijaspaligs.ui

import androidx.lifecycle.ViewModel
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.AppRepository
import com.davisdabols.inventarizacijaspaligs.data.models.WarehouseModel
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
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
    val loggedInUser = _loggedInUser.asStateFlow()
    val error = _error.asSharedFlow()
    val logInStatus = _logInStatus.asSharedFlow()
    val warehouses = _warehouses.asSharedFlow()

    var selectedWarehouse: WarehouseModel? = null

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
            Timber.d("Test: %s", loggedInUser)
            val workerModel = repository.getWarehouses(loggedInUser.value!!.AdminID)
            if (workerModel != null) {
                _warehouses.emit(workerModel)
            }
        }
    }
}
