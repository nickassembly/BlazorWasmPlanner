using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components.ToDoItems
{
    public partial class ToDoItem
    {
        [Parameter]
        public ToDoItemDetail Item { get; set; }
    }
}
