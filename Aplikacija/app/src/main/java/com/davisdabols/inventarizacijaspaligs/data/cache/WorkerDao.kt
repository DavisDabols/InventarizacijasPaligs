package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.*
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import kotlinx.coroutines.flow.Flow

@Dao
interface WorkerDao {
    @Query("SELECT * FROM WORKER_TABLE LIMIT 1")
    suspend fun getWorker(): WorkerModel?

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertWorker(workerModel: WorkerModel)

    @Query("SELECT EXISTS(SELECT 1 FROM WORKER_TABLE)")
    suspend fun existsWorker(): Boolean

    @Query("DELETE FROM WORKER_TABLE")
    suspend fun deleteWorker()
}
