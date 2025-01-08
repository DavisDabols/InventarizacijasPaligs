package com.davisdabols.inventarizacijaspaligs.ui.edititem

import DecimalDigitsInputFilter
import android.os.Bundle
import android.text.TextUtils
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.core.text.set
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

        binding.itemPriceInput.filters = arrayOf(DecimalDigitsInputFilter(10, 2))

        binding.itemBarcodeInput.setText(viewModel.selectedItem!!.Barcode)
        binding.itemTitleInput.setText(viewModel.selectedItem!!.Name)
        binding.itemDescriptionInput.setText(viewModel.selectedItem!!.Description)
        binding.itemCountInput.setText(viewModel.selectedItem!!.Count.toString())
        binding.itemPriceInput.setText(viewModel.selectedItem!!.Price.toString())

        binding.closeEditItems.setOnClickListener {
            openFragment(R.id.navigation_view_item)
        }

        binding.editItem.setOnClickListener {
            if(TextUtils.isEmpty(binding.itemTitleInput.text.toString()))
            {
                Toast.makeText(context, "Lauks nosaukums ir obligāts", Toast.LENGTH_SHORT).show()
            }
            else if (TextUtils.isEmpty(binding.itemCountInput.text.toString()))
            {
                Toast.makeText(context, "Lauks skaits ir obligāts", Toast.LENGTH_SHORT).show()
            }
            else if (TextUtils.isEmpty(binding.itemPriceInput.text.toString()))
            {
                Toast.makeText(context, "Lauks cena ir obligāts", Toast.LENGTH_SHORT).show()
            }
            else if (binding.itemCountInput.text.toString().toInt() < 1 || binding.itemCountInput.text.toString().toInt() > 1000000)
            {
                Toast.makeText(context, "Skaitam jābūt starp 1 un 1000000", Toast.LENGTH_SHORT).show()
            }
            else
            {
                viewModel.updateItems(
                    binding.itemBarcodeInput.text.toString(),
                    binding.itemTitleInput.text.toString(),
                    binding.itemDescriptionInput.text.toString(),
                    binding.itemCountInput.text.toString().toInt(),
                    binding.itemPriceInput.text.toString().toFloat(),
                    viewModel.selectedItem!!.WarehouseID,
                    'E')

                openFragment(R.id.navigation_items_list)
            }
        }
    }
}