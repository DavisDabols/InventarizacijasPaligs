package com.davisdabols.inventarizacijaspaligs.data.models

import androidx.room.Entity
import androidx.room.PrimaryKey
import com.davisdabols.inventarizacijaspaligs.common.WORKER_TABLE
import com.google.gson.annotations.SerializedName
import kotlinx.serialization.SerialName
import kotlinx.serialization.Serializable

@Entity(tableName = WORKER_TABLE)
data class WorkerModel (
    @SerializedName("id") @PrimaryKey val ID : String,
    @SerializedName("name") val Name : String,
    @SerializedName("surname") val Surname : String,
    @SerializedName("email") val Email : String,
    @SerializedName("password") val Password : String,
    @SerializedName("userId") val AdminID : String,
)

/*

[
    {
        "id": "f54ecbeb-8e93-4fa0-9b9d-e95108595c9d",
        "name": "test",
        "surname": "Kalniņš",
        "email": "a@a",
        "password": "$2a$11$F.8j/oDNy.vPeNXy0B6PiuU1t4RrZ/.NHBYuxvsrazaKDVkIkDpBS",
        "userId": "01bdbed7-73d0-448b-a8ba-a338a2e98f20"
    }
]
 */
