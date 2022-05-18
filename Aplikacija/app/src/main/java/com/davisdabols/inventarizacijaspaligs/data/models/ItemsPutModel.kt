package com.davisdabols.inventarizacijaspaligs.data.models

import com.google.gson.annotations.SerializedName

data class ItemsPutModel(
    @SerializedName("name") val Name : String,
    @SerializedName("description") val Description : String?,
    @SerializedName("count") val Count : Int,
    @SerializedName("price") val Price : Float,
    @SerializedName("warehouseId") val WarehouseID : String,
)
