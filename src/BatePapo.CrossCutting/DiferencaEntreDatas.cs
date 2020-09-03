using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.CrossCutting
{
    public class DiferencaEntreDatas
    {
        /// <summary>
        /// defining Number of days in month; index 0=> january and 11=> December
        /// february contain either 28 or 29 days, that's why here value is -1
        /// which wil be calculate later.
        /// </summary>
        private int[] diasMes = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// contain from date
        /// </summary>
        private DateTime daData;

        /// <summary>
        /// contain To Date
        /// </summary>
        private DateTime paraData;

        /// <summary>
        /// this three variable for output representation..
        /// </summary>
        private int ano;
        private int mes;
        private int dia;

        public DiferencaEntreDatas(DateTime d1, DateTime d2)
        {
            int increment;

            if (d1 > d2)
            {
                this.daData = d2;
                this.paraData = d1;
            }
            else
            {
                this.daData = d1;
                this.paraData = d2;
            }

            /// 
            /// Day Calculation
            /// 
            increment = 0;

            if (this.daData.Day > this.paraData.Day)
            {
                increment = this.diasMes[this.daData.Month - 1];

            }
            /// if it is february month
            /// if it's to day is less then from day
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(this.daData.Year))
                {
                    // leap year february contain 29 days
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {
                dia = (this.paraData.Day + increment) - this.daData.Day;
                increment = 1;
            }
            else
            {
                dia = this.paraData.Day - this.daData.Day;
            }

            ///
            ///month calculation
            ///
            if ((this.daData.Month + increment) > this.paraData.Month)
            {
                this.mes = (this.paraData.Month + 12) - (this.daData.Month + increment);
                increment = 1;
            }
            else
            {
                this.mes = (this.paraData.Month) - (this.daData.Month + increment);
                increment = 0;
            }

            ///
            /// year calculation
            ///
            this.ano = this.paraData.Year - (this.daData.Year + increment);

        }

        public override string ToString()
        {
            //return base.ToString();
            return this.ano + " Year(s), " + this.mes + " month(s), " + this.dia + " day(s)";
        }

        public int Years
        {
            get
            {
                return this.ano;
            }
        }

        public int Months
        {
            get
            {
                return this.mes;
            }
        }

        public int Days
        {
            get
            {
                return this.dia;
            }
        }

    }
}
