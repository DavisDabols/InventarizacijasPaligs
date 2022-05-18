package com.davisdabols.inventarizacijaspaligs.data.models

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.davisdabols.inventarizacijaspaligs.common.ITEMS_TABLE
import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

@Entity(tableName = ITEMS_TABLE)
data class ItemsModel (
    @SerializedName("id") @PrimaryKey val ID : String,
    @SerializedName("name") val Name : String,
    @SerializedName("description") val Description : String?,
    @SerializedName("count") val Count : Int,
    @SerializedName("price") val Price : Float,
    @SerializedName("warehouseId") val WarehouseID : String,
    @SerializedName("userId") val AdminId : String,
)
{
    val itemPrice get() = Price.toString()
    val itemCount get() = Count.toString()
}
/*
{
        "id": "8c948b3f-6a8a-4c1c-a1d3-cbd56afb4898",
        "name": "test",
        "description": "test",
        "createdDateTime": "2022-02-14T14:36:45.4635129",
        "warehouseId": "bb7d41e1-6a60-43e1-8a67-3782648fbd17",
        "userId": "01bdbed7-73d0-448b-a8ba-a338a2e98f20"
    }
*/