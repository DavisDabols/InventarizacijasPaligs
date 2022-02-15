package com.davisdabols.inventarizacijaspaligs.data

import com.davisdabols.inventarizacijaspaligs.data.models.Worker
import org.json.JSONArray
import org.json.JSONTokener
import timber.log.Timber
import java.net.URL

class Request {

    fun checkLogin(email: String, password: String): Worker {
        val json =
            JSONTokener(URL("https://invpalapi.azurewebsites.net/workeritems/email/$email/password/$password").readText()).nextValue() as JSONArray
        val worker = Worker(
            json.getJSONObject(0).getString("Id"),
            json.getJSONObject(0).getString("Name"),
            json.getJSONObject(0).getString("Surname"),
            json.getJSONObject(0).getString("Email"),
            json.getJSONObject(0).getString("UserId")
        )
        Timber.d("Worker: %s", worker)
        return worker
    }

}