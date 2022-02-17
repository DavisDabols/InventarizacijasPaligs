package com.davisdabols.inventarizacijaspaligs.data

import com.davisdabols.inventarizacijaspaligs.data.cache.AppDatabase
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import com.davisdabols.inventarizacijaspaligs.data.networking.WorkerApi
import timber.log.Timber
import javax.inject.Inject

class AppRepository @Inject constructor(
    private val api: WorkerApi,
    private val db: AppDatabase,
) {
    suspend fun checkLoggedIn(): Boolean {
        val exists = db.workerDao().existsWorker()
        Timber.d("exists: %s", exists)
        return exists
    }

    suspend fun checkLogin(email: String, password: String): WorkerModel {
        val worker = api.getWorker(email, password)
        db.workerDao().insertWorker(worker)
        return worker
    }

    suspend fun logOut() {
        db.workerDao().deleteWorker()
    }
}
