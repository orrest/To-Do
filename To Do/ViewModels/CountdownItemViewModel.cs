using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;

namespace To_Do.ViewModels
{
    public class CountdownItemViewModel : BindableBase
    {
        #region Data
        private long id;
		public long Id
		{
			get { return id; }
			set { id = value; RaisePropertyChanged(); }
		}

		private string icon;
		public string Icon
		{
			get { return icon; }
			set { icon = value; RaisePropertyChanged(); }
		}

		private string description;
		public string Description
		{
			get { return description; }
			set { description = value; RaisePropertyChanged(); }
		}

		private DateTime endDate;
		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}

		private DateTime startDate;
		public DateTime StartDate
		{
			get { return startDate; }
			set { startDate = value; RaisePropertyChanged(); }
		}

		private bool isStared;
		public bool IsStared
		{
			get { return isStared; }
			set { isStared = value; RaisePropertyChanged(); }
		}

		private DateTime createTime;
		public DateTime CreateTime
		{
			get { return createTime; }
			set { createTime = value; RaisePropertyChanged(); }
		}

		private DateTime updateTime;
		public DateTime UpdateTime
		{
			get { return updateTime; }
			set { updateTime = value; RaisePropertyChanged(); }
		}
        #endregion

		/// <summary>
		/// DateTime.Now > EndDate ?
		/// </summary>
        private bool isOver;
        public bool IsOver
        {
            get { return isOver; }
            set { isOver = value; RaisePropertyChanged(); }
        }

		/// <summary>
		/// (DateTime.Now - StartDate) / (EndDate - StartDate)
		/// </summary>
		private double progress;
		public double Progress
		{
			get { return progress; }
			set { progress = value; RaisePropertyChanged(); }
		}

		public CountdownItemViewModel()
        {
            Enum.GetName(PackIconKind.Add);
			this.IsOver = DateTime.Now >= EndDate;
			this.Progress = (DateTime.Now.Date - StartDate.Date)
				/ (EndDate.Date - StartDate.Date);
        }


    }
}
