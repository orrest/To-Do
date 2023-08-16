using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using System;
using To_Do.Shared;

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

		private double CalcProgress()
		{
			var now = DateTime.Now;
			if (now.Date > EndDate.Date) return 100.0;
            return 100 * (now.Date - StartDate.Date) / (EndDate.Date - StartDate.Date);
        }

        public CountdownItemViewModel()
        {
            Enum.GetName(PackIconKind.Add);
			this.IsOver = DateTime.Now >= EndDate;
			this.Progress = CalcProgress();
        }

        public CountdownItemViewModel(CountdownDTO dto)
        {
			this.Id = dto.Id;
			this.Icon = dto.Icon;
			this.Description = dto.Description;
			this.EndDate = dto.EndDate;
			this.StartDate = dto.StartDate;
			this.CreateTime = dto.CreateTime;
			this.UpdateTime = dto.UpdateTime;
			var now = DateTime.Now;
			this.IsOver = now.Date > EndDate.Date;
			this.Progress = CalcProgress();
        }
    }
}
