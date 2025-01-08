package com.davisdabols.inventarizacijaspaligs.ui.viewitem

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.fragment.app.activityViewModels
import com.davisdabols.inventarizacijaspaligs.R
import com.davisdabols.inventarizacijaspaligs.common.openFragment
import com.davisdabols.inventarizacijaspaligs.databinding.FragmentViewItemBinding
import com.davisdabols.inventarizacijaspaligs.ui.AppViewModel

class ViewItemFragment : Fragment() {

    private lateinit var binding: FragmentViewItemBinding

    private val viewModel by activityViewModels<AppViewModel>()

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View {
        binding = FragmentViewItemBinding.inflate(inflater)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        binding.item = viewModel.selectedItem

        binding.deleteItem.setOnClickListener {
            viewModel.deleteItems()
            openFragment(R.id.navigation_items_list)
        }

        binding.editItem.setOnClickListener {
            openFragment(R.id.navigation_edit_item)
        }

        binding.moveItem.setOnClickListener {
            openFragment(R.id.navigation_move_item)
        }
    }
}