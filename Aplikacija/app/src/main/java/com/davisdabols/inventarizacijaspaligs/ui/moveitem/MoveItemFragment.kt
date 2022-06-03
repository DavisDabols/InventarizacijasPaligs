package com.davisdabols.inventarizacijaspaligs.ui.moveitem

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import androidx.lifecycle.lifecycleScope
import com.davisdabols.inventarizacijaspaligs.R
import com.davisdabols.inventarizacijaspaligs.common.launchIO
import com.davisdabols.inventarizacijaspaligs.common.launchMain
import com.davisdabols.inventarizacijaspaligs.common.openFragment
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentWarehousesBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import com.davisdabols.inventarizacijaspaligs.ui.WarehouseState
import kotlinx.coroutines.flow.collect
import kotlinx.coroutines.flow.collectLatest
import timber.log.Timber

class MoveItemFragment: Fragment() {
    private lateinit var binding: FragmentWarehousesBinding

    private val viewModel by activityViewModels<AppViewModel>()

    private val adapter by lazy {
        MoveItemAdapter { warehouse ->
            Timber.d("Note item clicked: $warehouse")
            viewModel.selectedWarehouse = warehouse
            viewModel.updateItems(
                viewModel.selectedItem!!.Name,
                viewModel.selectedItem!!.Description,
                viewModel.selectedItem!!.Count,
                viewModel.selectedItem!!.Price,
                warehouse.ID,
                'M'
            )
            openFragment(R.id.navigation_items_list)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentWarehousesBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.emptyWarehouse.visibility = View.GONE

        viewModel.getWarehouses()

        binding.warehouseList.adapter = adapter

        binding.closeWarehouses.setOnClickListener {
            openFragment(R.id.navigation_view_item)
        }

        lifecycleScope.launchWhenCreated {
            viewModel.warehouseState.collectLatest { state ->
                when(state) {
                    WarehouseState.IsEmpty -> {
                        binding.emptyWarehouse.visibility = View.VISIBLE
                        binding.loading.visibility = View.GONE
                    }
                    WarehouseState.Loading -> {
                        binding.loading.visibility = View.VISIBLE
                        binding.emptyWarehouse.visibility = View.GONE
                    }
                    is WarehouseState.Warehouses -> {
                        adapter.warehouseList = state.warehouses
                        binding.loading.visibility = View.GONE
                    }
                }
            }
        }
    }
}