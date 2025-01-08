package com.davisdabols.inventarizacijaspaligs.ui.itemslist

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.davisdabols.inventarizacijaspaligs.R
import com.davisdabols.inventarizacijaspaligs.common.openFragment
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentItemsBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import com.davisdabols.inventarizacijaspaligs.ui.ItemsState
import dagger.hilt.android.AndroidEntryPoint
import kotlinx.coroutines.flow.collectLatest
import timber.log.Timber

@AndroidEntryPoint
class ItemsListFragment : Fragment() {
    private lateinit var binding: FragmentItemsBinding

    private val viewModel by activityViewModels<AppViewModel>()

    private val adapter by lazy {
        ItemsListAdapter { items ->
            Timber.d("Note item clicked: $items")
            viewModel.selectedItem = items
            openFragment(R.id.navigation_view_item)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentItemsBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.loading.visibility = View.VISIBLE

        viewModel.selectedItem = null;

        viewModel.getItems()

        binding.itemList.adapter = adapter

        binding.closeItemsList.setOnClickListener {
            viewModel.selectedWarehouse = null
            openFragment(R.id.navigation_warehouses)
        }

        binding.addItem.setOnClickListener {
            openFragment(R.id.navigation_add_items)
        }

        lifecycleScope.launchWhenCreated {
            viewModel.itemsState.collectLatest { state ->
                when(state) {
                    ItemsState.IsEmpty -> {
                        adapter.itemsList = emptyList()
                        binding.emptyItems.visibility = View.VISIBLE
                        binding.loading.visibility = View.GONE
                    }
                    ItemsState.Loading -> {
                        binding.loading.visibility = View.VISIBLE
                        binding.emptyItems.visibility = View.GONE
                    }
                    is ItemsState.Items -> {
                        adapter.itemsList = state.items
                        binding.loading.visibility = View.GONE
                    }
                }
            }
        }
    }
}