package com.davisdabols.inventarizacijaspaligs.ui.warehouse

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
import kotlinx.coroutines.flow.collect
import timber.log.Timber

class WarehouseFragment: Fragment() {
    private lateinit var binding: FragmentWarehousesBinding

    private val viewModel by activityViewModels<AppViewModel>()

    private val adapter by lazy {
        WarehouseAdapter { warehouse ->
            Timber.d("Note item clicked: $warehouse")
            viewModel.selectedWarehouse = warehouse
            //openFragment(R.id.navigation_warehouse_items)
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

        viewModel.getWarehouses()

        binding.warehouseList.adapter = adapter

        binding.closeWarehouses.setOnClickListener {
            openFragment(R.id.navigation_menu)
        }

        lifecycleScope.launchWhenCreated {
            viewModel.warehouses.collect() { warehouses ->
                binding.emptyWarehouse.visibility = if (warehouses.isEmpty()) {
                    View.VISIBLE
                } else {
                    View.GONE
                }
                adapter.warehouseList = warehouses
            }
        }
    }
}