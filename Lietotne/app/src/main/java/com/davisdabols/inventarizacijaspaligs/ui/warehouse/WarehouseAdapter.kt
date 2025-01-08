package com.davisdabols.inventarizacijaspaligs.ui.warehouse

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.RecyclerView
import com.davisdabols.inventarizacijaspaligs.data.models.WarehouseModel
import com.davisdabols.inventarizacijaspaligs.databinding.ItemWarehouseBinding
import kotlin.properties.Delegates

class WarehouseAdapter(private val onItemClick: (warehouseModel: WarehouseModel) -> Unit) : RecyclerView.Adapter<WarehouseAdapter.ViewHolder>() {

    var warehouseList: List<WarehouseModel> by Delegates.observable(emptyList(), { _, old, new ->
        DiffUtil.calculateDiff(DifferenceUtil(old, new)).dispatchUpdatesTo(this)
    })

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int) = ViewHolder(
        ItemWarehouseBinding.inflate(
            LayoutInflater.from(parent.context),
            parent,
            false
        )
    )

    override fun onBindViewHolder(holder: WarehouseAdapter.ViewHolder, position: Int) {
        val item = warehouseList[position]
        holder.binding.warehouseItem.tag = item
        holder.binding.item = item

        holder.binding.warehouseItem.setOnClickListener { cardView ->
            onItemClick(cardView.tag as WarehouseModel)
        }
    }

    override fun getItemCount() = warehouseList.size

    inner class ViewHolder(val binding: ItemWarehouseBinding) : RecyclerView.ViewHolder(binding.root)

    inner class DifferenceUtil(private val old: List<WarehouseModel>, private val new: List<WarehouseModel>) : DiffUtil.Callback() {
        override fun getOldListSize() = old.size

        override fun getNewListSize() = new.size

        override fun areItemsTheSame(oldItemPosition: Int, newItemPosition: Int) =
            old[oldItemPosition].ID == new[newItemPosition].ID

        override fun areContentsTheSame(oldItemPosition: Int, newItemPosition: Int) =
            old[oldItemPosition] == new[newItemPosition]
    }
}