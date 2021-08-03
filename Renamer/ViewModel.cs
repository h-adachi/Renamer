using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Renamer
{
	class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<String> Items { get; set; } = new ObservableCollection<String>();
		public List<List<RenameItem>> BackupItemList { get; set; } = new List<List<RenameItem>>();

		public String Path
		{
			get { return mPath; }
			set
			{
				mPath = value;
				UpdateItem();
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
			}
		}

		public uint Type
		{
			get { return (uint)mType; }
			set
			{
				mType = (Util.eItemType)value;
				UpdateItem();
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
			}
		}

		public int StartIndex
		{
			get { return mStartIndex; }
			set
			{
				mStartIndex = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StartIndex)));
			}
		}

		public int EndIndex
		{
			get { return mEndIndex; }
			set
			{
				mEndIndex = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EndIndex)));
			}
		}

		public int MiddleIndex
		{
			get { return mMiddleIndex; }
			set
			{
				mMiddleIndex = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MiddleIndex)));
			}
		}

		public int MiddleLength
		{
			get { return mMiddleLength; }
			set
			{
				mMiddleLength = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MiddleLength)));
			}
		}

		public bool IsExtension
		{
			get { return mIsExtension; }
			set
			{
				mIsExtension = value;
				UpdateItem();
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExtension)));
			}
		}


		private void UpdateItem()
		{
			Items.Clear();
			if (!System.IO.Directory.Exists(mPath)) return;

			if (mType != Util.eItemType.ItemType_File)
			{
				foreach (var dir in System.IO.Directory.GetDirectories(mPath))
				{
					Items.Add(System.IO.Path.GetFileName(dir));
				}
			}

			if (mType != Util.eItemType.ItemType_Dir)
			{
				foreach (var file in System.IO.Directory.GetFiles(mPath))
				{
					var name = System.IO.Path.GetFileName(file);
					if (!mIsExtension)
					{
						name = System.IO.Path.GetFileNameWithoutExtension(name);
					}
					if (String.IsNullOrEmpty(name)) continue;

					Items.Add(name);
				}
			}
		}

		private String mPath;
		private Util.eItemType mType;

		private int mStartIndex;
		private int mEndIndex;

		private int mMiddleIndex;
		private int mMiddleLength;

		private bool mIsExtension = true;
	}
}
