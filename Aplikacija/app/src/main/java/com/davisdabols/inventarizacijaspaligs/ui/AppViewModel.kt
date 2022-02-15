package com.davisdabols.inventarizacijaspaligs.ui

import androidx.lifecycle.ViewModel
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.Request
import com.davisdabols.inventarizacijaspaligs.data.models.Worker
import kotlinx.coroutines.flow.MutableSharedFlow
import kotlinx.coroutines.flow.SharedFlow
import timber.log.Timber

class AppViewModel : ViewModel() {

    private val _loggedInUser = MutableSharedFlow<Worker>(replay = 1)

    val loggedInUser: SharedFlow<Worker> = _loggedInUser

    fun logIn(email: String, password: String) {
        launchIO {
            val worker: Worker = Request().checkLogin(email, password)
            _loggedInUser.tryEmit(worker)
        }
    }

}