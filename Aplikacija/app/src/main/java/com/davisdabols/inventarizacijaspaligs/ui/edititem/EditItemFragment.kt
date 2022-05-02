package com.davisdabols.inventarizacijaspaligs.ui.edititem

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.davisdabols.inventarizacijaspaligs.R
import com.davisdabols.inventarizacijaspaligs.common.openFragment
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentEditItemBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class EditItemFragment : Fragment() {
    private lateinit var binding: FragmentEditItemBinding

    private val viewModel by activityViewModels<AppViewModel>()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = FragmentEditItemBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.closeEditItems.setOnClickListener {
            openFragment(R.id.navigation_view_item)
        }

        binding.editItem.setOnClickListener {
            viewModel.updateItems(
                binding.itemTitleInput.text.toString(),
                binding.itemDescriptionInput.text.toString(),
                viewModel.selectedItem!!.WarehouseID
            )
            openFragment(R.id.navigation_items_list)
        }
    }
}