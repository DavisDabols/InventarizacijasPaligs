package com.davisdabols.inventarizacijaspaligs.ui.additem

import DecimalDigitsInputFilter
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.Lifecycle
import androidx.lifecycle.lifecycleScope
import com.davisdabols.inventarizacijaspaligs.R
import com.davisdabols.inventarizacijaspaligs.common.openFragment
import com.davisdabols.inventarizacijaspaligs.data.models.ItemsModel
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentAddItemsBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collectLatest

@AndroidEntryPoint
class AddItemFragment : Fragment() {
    private lateinit var binding: FragmentAddItemsBinding

    private val viewModel by activityViewModels<AppViewModel>()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = FragmentAddItemsBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.itemPriceInput.filters = arrayOf(DecimalDigitsInputFilter(10, 2))

        binding.addNewItem.setOnClickListener {
            viewModel.postItems(
                binding.itemBarcodeInput.text.toString(),
                binding.itemTitleInput.text.toString(),
                binding.itemDescriptionInput.text.toString(),
                binding.itemCountInput.text.toString().toInt(),
                binding.itemPriceInput.text.toString().toFloat()
            )
            openFragment(R.id.navigation_items_list)
        }

        binding.closeAddItems.setOnClickListener {
            openFragment(R.id.navigation_items_list)
        }

        lifecycleScope.launchWhenCreated {
            viewModel.error.collectLatest { error ->
                Toast.makeText(context, error, Toast.LENGTH_SHORT).show()
            }
        }
    }
}