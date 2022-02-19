package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import com.davisdabols.inventarizacijaspaligs.data.models.ItemsModel
import kotlinx.coroutines.flow.Flow

@Dao
interface ItemsDao {
    @Query("SELECT * FROM ITEMS_TABLE")
    fun getItems(): Flow<List<ItemsModel>>

    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertItems(itemsModel: ItemsModel)

    @Query("DELETE FROM ITEMS_TABLE")
    fun deleteItems()
}