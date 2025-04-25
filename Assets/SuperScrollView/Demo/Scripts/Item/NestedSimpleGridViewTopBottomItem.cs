﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
    public class NestedSimpleGridViewTopBottomItem : MonoBehaviour
    {
        public LoopGridView mLoopGridView;
        public Text mTitle;
        int mIndex;
        DataSourceMgr<SimpleItemData> mDataSourceMgr;

        public void Init()
        {
            mLoopGridView.InitGridView(0, OnGetItemByRowColumn);     
        }

        public void SetItemData(NestedSimpleItemData itemData)
        {
            mIndex = itemData.mIndex;
            mTitle.text = itemData.mName;
            mDataSourceMgr = itemData.mDataSourceMgr;
            mLoopGridView.ClearAllShownItems();
            mLoopGridView.SetListItemCount(mDataSourceMgr.TotalItemCount);
            mLoopGridView.MovePanelToItemByIndex(0, 0);
            mLoopGridView.RefreshAllShownItem();
        }


        LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex,int row,int column)
        {
            if (itemIndex < 0)
            {
                return null;
            }

            //get the data to showing
            SimpleItemData itemData = mDataSourceMgr.GetItemDataByIndex(itemIndex);
            if (itemData == null)
            {
                return null;
            }
            /*
            get a new item. Every item can use a different prefab, 
            the parameter of the NewListViewItem is the prefab’name. 
            And all the prefabs should be listed in ItemPrefabList in LoopGridView Inspector Setting
            */
            LoopGridViewItem item = gridView.NewListViewItem("ItemPrefab");

            SimpleItem itemScript = item.GetComponent<SimpleItem>();//get your own component
            // IsInitHandlerCalled is false means this item is new created but not fetched from pool.
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();// here to init the item, such as add button click event listener.
            }
            //update the item’s content for showing, such as image,text.
            itemData.mName = "Item"+ mIndex + "_" + itemIndex;
            itemScript.SetItemData(itemData);
            return item;
        }     
    }
}
