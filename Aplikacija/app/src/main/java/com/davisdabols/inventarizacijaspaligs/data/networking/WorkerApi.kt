package com.davisdabols.inventarizacijaspaligs.data.networking

import com.davisdabols.inventarizacijaspaligs.data.models.WorkerModel
import retrofit2.http.GET
import retrofit2.http.Path

interface WorkerApi {
    @GET("workeritems/email/{email}/password/{password}")
    suspend fun getWorker(
        @Path("email") email: String,
        @Path("password") password: String
    ): WorkerModel
}
