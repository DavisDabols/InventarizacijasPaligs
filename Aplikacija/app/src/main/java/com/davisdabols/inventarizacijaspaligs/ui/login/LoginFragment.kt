package com.davisdabols.inventarizacijaspaligs.ui.login

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentLoginBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch

@AndroidEntryPoint
class LoginFragment : Fragment() {
    private lateinit var binding: FragmentLoginBinding

    private val viewModel by activityViewModels<AppViewModel>()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentLoginBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.submitInput.setOnClickListener {
            viewModel.logIn(
                binding.emailInput.text.toString(),
                binding.passwordInput.text.toString()
            )
        }

        lifecycleScope.launchWhenCreated {
            viewModel.loggedInUser.collectLatest { worker ->
                if (worker != null) {
                    Toast.makeText(context, "$worker", Toast.LENGTH_SHORT)
                        .show()
                }
            }
        }

        lifecycleScope.launchWhenCreated {
            viewModel.error.collectLatest { error ->
                Toast.makeText(context, error, Toast.LENGTH_SHORT).show()
            }
        }
    }
}
