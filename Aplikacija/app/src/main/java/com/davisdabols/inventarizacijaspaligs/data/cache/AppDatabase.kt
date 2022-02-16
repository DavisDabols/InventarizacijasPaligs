package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.Database
import androidx.room.RoomDatabase
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel

@Database(entities = [WorkerModel::class], version = 1, exportSchema = false)
abstract class AppDatabase : RoomDatabase() {
    abstract fun workerDao(): WorkerDao
}
