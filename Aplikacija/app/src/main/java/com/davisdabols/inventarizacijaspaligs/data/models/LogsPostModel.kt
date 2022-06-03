package com.davisdabols.inventarizacijaspaligs.data.models

import com.google.gson.annotations.SerializedName

data class LogsPostModel (
    @SerializedName("name") val Name : String,
    @SerializedName("surname") val Surname : String?,
    @SerializedName("userId") val AdminId : String,
    @SerializedName("itemName") val ItemName : String,
    @SerializedName("action") val Action : Char,
)