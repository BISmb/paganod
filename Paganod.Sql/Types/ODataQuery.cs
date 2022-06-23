namespace Paganod.Sql.Types;

//{
//    public enum OrderBy
//    {
//        Asc,
//        Desc
//    }

//    public class GroupFilter
//    {
//        public int GroupId { get; set; }
//        public Enums.Data.FilterAndOr AndOr { get; set; }
//        public List<ODataFilter> Filters { get; set; }
//    }

//    public class ODataQuery
//    {
//        public string BaseTableName { get; set; }
//        public List<string> SelectFields { get; set; } = new();
//        public Dictionary<string, OrderBy> Order { get; set; } = new();
//        public int Skip { get; set; }
//        public int Top { get; set; }
//        public bool Count { get; set; }


//        public List<GroupFilter> GroupFilters { get; set; } = new();

//        public List<ODataFilter> Filters { get; set; } = new();

//        private int _GroupId { get; set; }
//        private Enums.Data.FilterAndOr _GroupAndOr { get; set; }

//        public ODataQuery(string strBaseTableName)
//        {
//            BaseTableName = strBaseTableName;
//        }

//        //public ODataQuery(string tableName, ODataQueryOptions<Record> oDataQueryOptions) 
//        //{
//        //    BaseTableName = tableName;
//        //    oDataQueryOptions.
//        //}

//        public ODataQuery Select(params string[] fieldsToSelect)
//        {
//            SelectFields.AddRange(fieldsToSelect);
//            return this;
//        }

//        //public ODataQuery OrderBy(string orderByField)
//        //{
//        //    OrderByField = orderByField;
//        //    return this;
//        //}

//        public ODataQuery OrderBy(string strOrderByField, OrderBy eOrder = Types.OrderBy.Asc)
//        {
//            Order.Add(strOrderByField, eOrder);
//            return this;
//        }

//        public ODataQuery OrderBy(params (string orderByField, Enums.Data.FilterOrder orderType)[] orderClauses)
//        {
//            Debugger.Break();
//            return this;
//        }

//        //public ODataQuery Group(Action<ODataQuery> additionalFilters)
//        //{
//        //    _GroupId = GroupFilters.Count;
//        //    additionalFilters(this);
//        //    return this;
//        //}

//        //public ODataQuery GroupAnd(Action<ODataQuery> additionalFilters)
//        //{
//        //    _GroupAndOr = Enums.Data.FilterAndOr.And;
//        //    Group(additionalFilters);
//        //    return this;
//        //}

//        //public ODataQuery GroupOr(Action<ODataQuery> additionalFilters)
//        //{
//        //    _GroupAndOr = Enums.Data.FilterAndOr.Or;
//        //    Group(additionalFilters);
//        //    return this;
//        //}

//        public IDictionary<string, string> GetDynamicDictionary()
//        {
//            IList<ODataFilter> orderedFilters = Filters.OrderBy(x => x._Index).ToList();
//            string strFinalFilter = "";

//            foreach (var group in GroupFilters)
//            {
//                if (group.AndOr != Enums.Data.FilterAndOr.NotSet)
//                    strFinalFilter += (group.AndOr == Enums.Data.FilterAndOr.And) ? " and " : " or ";

//                strFinalFilter += $"({String.Join(" ", group.Filters)})";
//            }

//            string strOrderBy = "";
//            for (int o = 0; o < Order.Count; o++)
//            {
//                var oOrderBy = Order.ElementAt(0);
//                if (o != 0) strOrderBy += ", ";
//                strOrderBy = $"{oOrderBy.Key} {oOrderBy.Value.ToString().ToLower()}";
//            }

//            return new Dictionary<string, string>()
//            {
//                { "table", BaseTableName },
//                { "select", string.Join(",", SelectFields) },
//                { "orderby", strOrderBy },
//                { "filter", strFinalFilter },
//                { "skip", $"{Skip}" },
//                { "top", $"{Top}" },
//            };
//        }

//        public ODataQuery WithPaging(int intPage, int intPageSize)
//        {
//            Skip = (intPage * intPageSize) - intPageSize;
//            Top = (intPage * intPageSize); // intPageSize;

//            return this;
//        }

//        public ODataQuery Where(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue)
//            => Add(strPropertyName, enumFilterType, oValue, false, Enums.Data.FilterAndOr.NotSet);

//        public ODataQuery And(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue)
//            => Add(strPropertyName, enumFilterType, oValue, false, Enums.Data.FilterAndOr.And);

//        public ODataQuery Or(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue)
//            => Add(strPropertyName, enumFilterType, oValue, false, Enums.Data.FilterAndOr.Or);

//        public ODataQuery AndNot(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue)
//            => Add(strPropertyName, enumFilterType, oValue, true, Enums.Data.FilterAndOr.And);

//        public ODataQuery OrNot(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue)
//            => Add(strPropertyName, enumFilterType, oValue, true, Enums.Data.FilterAndOr.Or);

//        private ODataQuery Add(string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue, bool not, Enums.Data.FilterAndOr andOr)
//        {
//            //Filters.Add(new ODataFilter(Filters.Count, strPropertyName, enumFilterType, oValue, not, andOr));

//            if (!GroupFilters.Where(x => x.GroupId == _GroupId).Any())
//                GroupFilters.Add(new GroupFilter() { GroupId = _GroupId, AndOr = _GroupAndOr, Filters = new() });

//            //var group = .First(x => x.GroupId == _GroupId);
//            GroupFilters[0].AndOr = _GroupAndOr;
//            GroupFilters[0].Filters.Add(new ODataFilter(Filters.Count, strPropertyName, enumFilterType, oValue, not, andOr));

//            return this;
//        }
//    }

//    public class ODataFilter
//    {
//        public int _Index { get; set; }

//        public string PropertyName { get; set; }
//        public Enums.Data.FilterType? FilterType { get; set; }
//        public object Value { get; set; }

//        public Enums.Data.FilterAndOr AndOr { get; set; }
//        public bool Not { get; set; }
//        public Enums.Design.FieldType TargetType { get; set; }

//        public ODataFilter() { }

//        public ODataFilter(int newIndex, string strPropertyName, Enums.Data.FilterType enumFilterType, object oValue, bool blnNot = false, Enums.Data.FilterAndOr andOr = Enums.Data.FilterAndOr.NotSet)
//        {
//            _Index = newIndex;

//            PropertyName = strPropertyName.ToLower();
//            FilterType = enumFilterType;
//            Value = oValue;
//            Not = blnNot;
//            AndOr = andOr;
//        }

//        public override string ToString()
//        {
//            IList<string> Fragments = new List<string>();

//            switch (AndOr)
//            {
//                case Enums.Data.FilterAndOr.And:
//                    Fragments.Add("and");
//                    break;
//                case Enums.Data.FilterAndOr.Or:
//                    Fragments.Add("or");
//                    break;

//                case Enums.Data.FilterAndOr.NotSet:
//                default:
//                    break;
//            };

//            if (Not) Fragments.Add("not");

//            if (Value.GetType() == typeof(string))
//                Value = $"'{Value}'";

//            if (Value.GetType() == typeof(string[]))
//                for (int i = 0; i < ((string[])Value).Length; i++)
//                    ((string[])Value)[i] = $"'{((string[])Value)[i]}'";

//            if (typeof(IEnumerable<>).IsAssignableFrom(Value.GetType()))
//                Value = string.Join(',', $"{(IEnumerable<object>)Value}");


//            string FilterValue = $"'{Value}'";

//            if (Int32.TryParse(Value.ToString(), out _)
//                || Int64.TryParse(Value.ToString(), out _)
//                || Decimal.TryParse(Value.ToString(), out _)
//                || Double.TryParse(Value.ToString(), out _)
//            )
//                FilterValue = $"{Value}";


//            //string FilterValue = Value switch
//            //{
//            //    DateTime => $"'{Value}'",
//            //    string => $"'{Value}'",
//            //    char => $"'{Value}'",

//            //    _ => $"{Value}",
//            //};

//            string FilterString = FilterType switch
//            {
//                Enums.Data.FilterType.BeginsWith => $"beginswith({PropertyName}, {FilterValue})",
//                Enums.Data.FilterType.EndsWith => $"endswith({PropertyName}, {FilterValue})",
//                Enums.Data.FilterType.In => $"{PropertyName} in ({FilterValue})",
//                Enums.Data.FilterType.Contains => $"contains({PropertyName},{FilterValue})",
//                Enums.Data.FilterType.Equals => $"{PropertyName} eq {FilterValue}",
//                Enums.Data.FilterType.NotEquals => $"{PropertyName} ne {FilterValue}",

//                Enums.Data.FilterType.GreaterThanOrEqual => $"{PropertyName} ge {FilterValue}",
//                Enums.Data.FilterType.GreaterThan => $"{PropertyName} gt {FilterValue}",
//                Enums.Data.FilterType.LessThanOrEqual => $"{PropertyName} le {FilterValue}",
//                Enums.Data.FilterType.LessThan => $"{PropertyName} lt {FilterValue}",

//                // (CustomerID eq null and length(Country) eq 0)
//                Enums.Data.FilterType.IsEmptyOrNull => $"({PropertyName} eq null and length({PropertyName}) eq 0)",



//                _ => throw new NotImplementedException(),
//            };

//            Fragments.Add(FilterString);

//            return String.Join(" ", Fragments);
//        }
//    }
//}

