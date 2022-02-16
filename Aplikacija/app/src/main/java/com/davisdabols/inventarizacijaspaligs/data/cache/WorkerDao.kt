package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.*
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import kotlinx.coroutines.flow.Flow

@Dao
interface WorkerDao {
    @Query("SELECT * FROM WORKER_TABLE")
    fun getWorker(): Flow<List<WorkerModel>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertWorker(workerModel: WorkerModel)
}
