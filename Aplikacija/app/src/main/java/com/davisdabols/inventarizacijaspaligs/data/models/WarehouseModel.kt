package com.davisdabols.inventarizacijaspaligs.data.models

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.davisdabols.inventarizacijaspaligs.common.WAREHOUSE_TABLE
import com.google.gson.annotations.SerializedName

@Entity(tableName = WAREHOUSE_TABLE)
data class WarehouseModel(
    @SerializedName("id") @PrimaryKey val ID : String,
    @SerializedName("name") val Name : String,
    @SerializedName("address") val Address : String,
    @SerializedName("maxCapacity") val MaxCapacity : Int,
    @SerializedName("userId") val AdminID : String,
)