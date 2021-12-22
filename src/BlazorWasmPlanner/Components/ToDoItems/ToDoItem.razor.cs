using BlazorWasmPlanner.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmPlanner.Components
{
    public partial class ToDoItem
    {
        [Parameter]
        public ToDoItemDetail Item { get; set; }

        private bool _isChecked = true;

        private bool _isEditMode = false;

        private string _description = "Welcome to Blazor & .Net";
    }
}
