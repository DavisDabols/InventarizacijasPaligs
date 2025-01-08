package com.davisdabols.inventarizacijaspaligs.data.cache

import androidx.room.*
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

    @Query("DELETE FROM ITEMS_TABLE WHERE ID = :id")
    suspend fun deleteSpecificItem(id: String)

    @Update
    fun updateItem(item: ItemsModel)
}