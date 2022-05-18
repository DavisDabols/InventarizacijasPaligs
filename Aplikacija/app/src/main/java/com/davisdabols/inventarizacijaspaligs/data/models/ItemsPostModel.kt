package com.davisdabols.inventarizacijaspaligs.data.models

import androidx.room.Entity
import com.davisdabols.inventarizacijaspaligs.common.ITEMS_TABLE
import com.google.gson.annotations.SerializedName

data class ItemsPostModel(
    @SerializedName("name") val Name : String,
    @SerializedName("description") val Description : String?,
    @SerializedName("count") val Count : Int,
    @SerializedName("price") val Price : Float,
    @SerializedName("warehouseId") val WarehouseID : String,
    @SerializedName("userId") val AdminId : String,
)
