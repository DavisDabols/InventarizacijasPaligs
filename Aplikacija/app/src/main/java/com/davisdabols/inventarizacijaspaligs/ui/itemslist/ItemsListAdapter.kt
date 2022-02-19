package com.davisdabols.inventarizacijaspaligs.ui.itemslist

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.davisdabols.inventarizacijaspaligs.data.models.ItemsModel
import com.davisdabols.inventarizacijaspaligs.databinding.ItemItemsBinding
import com.davisdabols.inventarizacijaspaligs.ui.warehouse.WarehouseAdapter
import kotlin.properties.Delegates

class ItemsListAdapter(private val onItemClick: (itemsModel: ItemsModel) -> Unit) : RecyclerView.Adapter<ItemsListAdapter.ViewHolder>() {

    var itemsList: List<ItemsModel> by Delegates.observable(emptyList(), { _, old, new ->
        DiffUtil.calculateDiff(DifferenceUtil(old, new)).dispatchUpdatesTo(this)
    })

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) = ViewHolder(
        ItemItemsBinding.inflate(
            LayoutInflater.from(parent.context),
            parent,
            false
        )
    )

    override fun onBindViewHolder(holder: ItemsListAdapter.ViewHolder, position: Int) {
        val item = itemsList[position]
        holder.binding.warehouseItem.tag = item
        holder.binding.item = item

        holder.binding.warehouseItem.setOnClickListener { cardView ->
            onItemClick(cardView.tag as ItemsModel)
        }
    }

    override fun getItemCount() = itemsList.size

    inner class ViewHolder(val binding: ItemItemsBinding) : RecyclerView.ViewHolder(binding.root)

    inner class DifferenceUtil(private val old: List<ItemsModel>, private val new: List<ItemsModel>) : DiffUtil.Callback() {
        override fun getOldListSize() = old.size

        override fun getNewListSize() = new.size

        override fun areItemsTheSame(oldItemPosition: Int, newItemPosition: Int) =
            old[oldItemPosition].ID == new[newItemPosition].ID

        override fun areContentsTheSame(oldItemPosition: Int, newItemPosition: Int) =
            old[oldItemPosition] == new[newItemPosition]
    }
}