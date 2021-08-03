using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Renamer
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_PreviewDragOver(object sender, DragEventArgs e)
		{
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
			var name = files[0];
			if (System.IO.File.Exists(name))
			{
				name = System.IO.Path.GetDirectoryName(name);
			}

			var vm = DataContext as ViewModel;
			if (vm == null) return;
			vm.Path = name;
		}

		private void RenameButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel;
			if (vm == null) return;
			if (!System.IO.Directory.Exists(vm.Path)) return;

			var renames = new List<RenameItem>();
			foreach (var item in ListBoxItem.SelectedItems)
			{
				var name = Util.CleateName(item.ToString(), vm.StartIndex, vm.EndIndex, vm.MiddleIndex, vm.MiddleLength);
				if (String.IsNullOrEmpty(name)) continue;

				var rename = new RenameItem();
				rename.Before = System.IO.Path.Combine(vm.Path, item.ToString());
				rename.After = System.IO.Path.Combine(vm.Path, name);
				if (System.IO.File.Exists(rename.Before))
				{
					System.IO.File.Move(rename.Before, rename.After);
				}
				else
				{
					System.IO.Directory.Move(rename.Before, rename.After);
				}
				renames.Add(rename);
			}
			vm.BackupItemList.Insert(0, renames);
			vm.Path = vm.Path;
		}

		private void RestoreButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel;
			if (vm == null) return;
			if (vm.BackupItemList.Count == 0) return;

			foreach (var item in vm.BackupItemList[0])
			{
				if (System.IO.File.Exists(item.After))
				{
					System.IO.File.Move(item.After, item.Before);
				}
				else
				{
					System.IO.Directory.Move(item.After, item.Before);
				}
			}
			vm.BackupItemList.RemoveAt(0);
			vm.Path = vm.Path;
		}

		private void OutputSameNameTextButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel;
			if (vm == null) return;
			if (!System.IO.Directory.Exists(vm.Path)) return;

			foreach (var item in ListBoxItem.SelectedItems)
			{
				var name = item.ToString();
				if(System.IO.File.Exists(System.IO.Path.Combine(vm.Path, name)))
				{
					name = System.IO.Path.GetFileNameWithoutExtension(name);
				}
				if (String.IsNullOrEmpty(name)) continue;

				name += ".txt";
				System.IO.File.WriteAllText(System.IO.Path.Combine(vm.Path, name), "");
			}
		}

		private void OutputListTextButton_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as ViewModel;
			if (vm == null) return;
			if (!System.IO.Directory.Exists(vm.Path)) return;

			
			var sb = new StringBuilder();
			foreach (var item in ListBoxItem.SelectedItems)
			{
				var name = item.ToString();
				if (System.IO.File.Exists(System.IO.Path.Combine(vm.Path, name)))
				{
					name = System.IO.Path.GetFileNameWithoutExtension(name);
				}
				if (String.IsNullOrEmpty(name)) continue;

				sb.AppendLine(name);
			}

			var path = System.IO.Path.GetFileName(vm.Path) + ".txt";
			System.IO.File.WriteAllText(System.IO.Path.Combine(vm.Path, path), sb.ToString());
		}
	}
}
