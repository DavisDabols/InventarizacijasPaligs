package com.davisdabols.inventarizacijaspaligs.common

import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import timber.log.Timber

fun Fragment.openFragment(id: Int) = findNavController().run {
    // Try to go back with destroying the existing fragment to stop flow collection
    if (!popBackStack(id, false)) {
        // If navigating back fails since a fragment not found in back stack - open the fragment instead
        navigate(id)
    }
}
