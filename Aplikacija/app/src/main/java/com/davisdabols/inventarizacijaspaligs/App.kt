package com.davisdabols.inventarizacijaspaligs

import android.app.Application
import com.davisdabols.inventarizacijaspaligs.common.LineNumberDebugThree
import timber.log.Timber

class App : Application() {
    override fun onCreate() {
        super.onCreate()
        if (BuildConfig.DEBUG) {
            Timber.plant(LineNumberDebugThree())
        }
        Timber.d("App created")
    }
}