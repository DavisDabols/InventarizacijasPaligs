package com.davisdabols.inventarizacijaspaligs.data.models

import androidx.room.Entity
import com.davisdabols.inventarizacijaspaligs.common.ITEMS_TABLE
import com.google.gson.annotations.SerializedName

@Entity(tableName = ITEMS_TABLE)
data class ItemsPostModel(
    @SerializedName("name") val Name : String,
    @SerializedName("description") val Description : String,
    @SerializedName("warehouseId") val WarehouseID : String,
    @SerializedName("userId") val AdminId : String,
)
