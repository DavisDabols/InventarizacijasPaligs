package com.davisdabols.inventarizacijaspaligs.data.networking

import com.davisdabols.inventarizacijaspaligs.data.models.ItemsModel
import com.davisdabols.inventarizacijaspaligs.data.models.ItemsPostModel
import com.davisdabols.inventarizacijaspaligs.data.models.WarehouseModel
import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import retrofit2.http.*

interface WorkerApi {
    @GET("workeritems/email/{email}/password/{password}")
    suspend fun getWorker(
        @Path("email") email: String,
        @Path("password") password: String
    ): WorkerModel

    @GET("warehouseitems/userId/{userId}")
    suspend fun getWarehouses(
        @Path("userId") AdminId: String
    ): List<WarehouseModel>

    @GET("itemsitems/warehouseId/{warehouseId}")
    suspend fun getItems(
        @Path("warehouseId") WarehouseId: String
    ): List<ItemsModel>

    @POST("itemsitems")
    suspend fun postItems(
        @Body item: ItemsPostModel
    )

    @DELETE("itemsitems/itemId/{itemId}")
    suspend fun deleteItems(
        @Path("itemId") ItemId: String
    )
}
