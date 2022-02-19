package com.davisdabols.inventarizacijaspaligs.data

import com.davisdabols.inventarizacijaspaligs.data.cache.AppDatabase
import com.davisdabols.inventarizacijaspaligs.data.models.ItemsModel
import com.davisdabols.inventarizacijaspaligs.data.models.WarehouseModel
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import com.davisdabols.inventarizacijaspaligs.data.networking.WorkerApi
import kotlinx.coroutines.flow.first
import timber.log.Timber
import javax.inject.Inject

class AppRepository @Inject constructor(
    private val api: WorkerApi,
    private val db: AppDatabase,
) {
    suspend fun checkLoggedIn(): WorkerModel? {
        val worker = db.workerDao().getWorker()
        return worker
    }

    suspend fun checkLogin(email: String, password: String): WorkerModel {
        val worker = api.getWorker(email, password)
        db.workerDao().insertWorker(worker)
        return worker
    }

    suspend fun logOut() {
        db.workerDao().deleteWorker()
        db.warehouseDao().deleteWarehouses()
    }

    suspend fun getWarehouses(adminId: String): List<WarehouseModel> {
        val warehouses = api.getWarehouses(adminId)
        warehouses.forEach { warehouse ->
            db.warehouseDao().insertWarehouse(warehouse)
        }
        return warehouses
    }

    suspend fun getItems(warehouseId: String): List<ItemsModel> {
        val items = api.getItems(warehouseId)
        items.forEach { item ->
            db.itemsDao().insertItems(item)
        }
        return items
    }
}
