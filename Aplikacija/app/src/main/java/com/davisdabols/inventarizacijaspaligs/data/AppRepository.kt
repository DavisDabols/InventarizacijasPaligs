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
    suspend fun checkLogin(email: String, password: String): WorkerModel {
        val worker = api.getWorker(email, password)
//        db.workerDao().insertWorker(worker)
        // TODO: fix you gosh darn API
        Timber.d("Worker: %s", worker)
        // TODO: this is bad but it is perfect for RVT
        return worker
    }
}
