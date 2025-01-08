package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import com.davisdabols.inventarizacijaspaligs.data.models.WarehouseModel
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import kotlinx.coroutines.flow.Flow

@Dao
interface WarehouseDao {
    @Query("SELECT * FROM WAREHOUSE_TABLE")
    fun getWarehouses(): Flow<List<WarehouseModel>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertWarehouse(warehouseModel: WarehouseModel)

    @Query("DELETE FROM WAREHOUSE_TABLE")
    fun deleteWarehouses()
}