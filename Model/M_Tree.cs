using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K.Y.DLL.Model
{
   public class M_TreeNode
   {
       public int NodeID { get; set; }
       public string NodeName { get; set; }
       public string NodeValue { get; set; }
       public string NodeDescrib { get; set; }
       public Nullable<int> ParentNodeID { get; set; }
       public Nullable<int> NodeLevel { get; set; }
       public Nullable<int> NodeSortNo { get; set; }
    }
}
