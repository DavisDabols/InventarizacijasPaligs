package com.davisdabols.inventarizacijaspaligs.ui

import androidx.lifecycle.ViewModel
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.AppRepository
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.*
import java.lang.Exception
import javax.inject.Inject

@HiltViewModel
class AppViewModel @Inject constructor(
    private val repository: AppRepository
) : ViewModel() {
    private val _loggedInUser = MutableStateFlow<WorkerModel?>(null)
    private val _error = MutableSharedFlow<String>()
    private val _logInStatus = MutableSharedFlow<Boolean>(replay=1)
    val loggedInUser = _loggedInUser.asStateFlow()
    val error = _error.asSharedFlow()
    val logInStatus = _logInStatus.asSharedFlow()

    fun checkLoggedIn() {
        launchIO {
            val logInStatus = repository.checkLoggedIn()
            _logInStatus.emit(logInStatus)
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
}
