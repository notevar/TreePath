using System;
using System.Collections.Generic;
using System.Linq;

namespace TreePath
{
    class Program
    {
        private static List<Tree> treeList = null;
        static Program()
        {
            //树结构 注意ID不能重复
            treeList = new List<Tree>() {
                new Tree() {  ID=1, Name="1", ParentID=null},
                new Tree() {  ID=2, Name="2", ParentID=1},
                new Tree() {  ID=3, Name="3", ParentID=1},
                new Tree() {  ID=4, Name="4", ParentID=1},
                new Tree() {  ID=5, Name="5", ParentID=2},
                new Tree() {  ID=6, Name="6", ParentID=2},
                new Tree() {  ID=7, Name="7", ParentID=4},
                new Tree() {  ID=8, Name="8", ParentID=4},
                new Tree() {  ID=9, Name="9", ParentID=5},
                new Tree() {  ID=10, Name="10", ParentID=7},
                new Tree() {  ID=11, Name="11", ParentID=7},
            };
        }
        static void Main(string[] args)
        {
            var treePath = GetChildTree(1);
            foreach (var item in treePath)
            {
                Console.WriteLine($"{item.ID},{string.Join(",", item.PathID)}");
            }
            Console.ReadKey();
        }

        /// <summary>
        /// 获取子级所有层级路径
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="allChildList"></param>
        /// <returns></returns>
        static List<TreePath> GetChildTree(int id)
        {
            var child = treeList.Where(c => c.ParentID == id).ToList();
            List<TreePath> treepath = new List<TreePath>();
            if (child != null && child.Count() > 0)
            {
                foreach (var item in child)
                {
                    var tempDic = GetChildTree(item.ID);
                    var tempList = new List<TreePath>();
                    if (tempDic == null || tempDic.Count == 0)
                    {
                        tempList = new List<TreePath>() { new TreePath() { ID = item.ParentID.Value, PathID = new List<int>() { item.ID } } };
                    }
                    else
                    {
                        var currentList = tempDic.Where(c => c.ID == item.ID);
                        foreach (var cur in currentList)
                        {
                            var tempCur = new TreePath() { ID = item.ParentID.Value, PathID = cur.PathID };
                            tempCur.PathID.Insert(0, cur.ID);
                            tempList.Add(tempCur);
                        }
                    }
                    treepath.AddRange(tempList);
                }
            }
            return treepath;
        }
    }


    class Tree
    {
        public int ID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int? ParentID { get; set; }
    }

    class TreePath
    {
        /// <summary>
        /// 跟节点树ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 路径集合
        /// </summary>
        public List<int> PathID { get; set; }
    }
}
