package com.davisdabols.inventarizacijaspaligs.ui.login

import android.app.AlertDialog
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.data.Request
import com.davisdabols.inventarizacijaspaligs.data.models.Worker
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentLoginBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import timber.log.Timber

class LoginFragment : Fragment() {

    private lateinit var binding: FragmentLoginBinding

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = FragmentLoginBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.submitInput.setOnClickListener {
            AppViewModel().logIn(
                binding.emailInput.text.toString(),
                binding.passwordInput.text.toString()
            )
        }
    }
}
