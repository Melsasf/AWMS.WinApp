using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPCAA.WinApp.Enums;
using WPCAA.WinApp.Models;
using WPCAA.WinApp.Properties;

namespace WPCAA.WinApp.Repositories
{

    public class TrackingRepository
    {
        private readonly string ApiAddress = Settings.Default.ApiAddress;
        public async Task<int> StartTrackingAsync(Tracking tracking, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/starttracking/", Method.Post);
            request.AddJsonBody(tracking);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            var response = await client.PostAsync(request);
            var trackingId = JsonConvert.DeserializeObject<int>(response.Content);
            return trackingId;
        }
        public async Task<int> EndTrackingAsync(Tracking tracking, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/endtracking/", Method.Put);
            request.AddJsonBody(tracking);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            var response = await client.PutAsync(request);
            var trackingId = JsonConvert.DeserializeObject<int>(response.Content);
            return trackingId;
        }

        public async Task<int> StartTrackingDetailAsync(TrackingDetail trackingDetail, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/starttrackingdetail/", Method.Post);
            request.AddJsonBody(trackingDetail);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            var response = await client.PostAsync(request);
            var trackingId = JsonConvert.DeserializeObject<int>(response.Content);
            return trackingId;
        }

        public async Task<int> EndTrackingDetailAsync(TrackingDetail trackingDetail, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/endtrackingdetail/", Method.Put);
            request.AddJsonBody(trackingDetail);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            var response = await client.PutAsync(request);
            var trackingId = JsonConvert.DeserializeObject<int>(response.Content);
            return trackingId;
        }

        public async Task<List<TrackingDto>> GetAllTrackingsById(int userId, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/GetAllTrackingsById", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            request.AddParameter("Id", userId);

            var response = await client.ExecuteGetAsync(request);

            var list = JsonConvert.DeserializeObject<List<TrackingDto>>(response.Content);

            return list;
        }

        public async Task<List<TrackingDetail>> GetAllTrackingDetailsByTrackingIds(List<int> trackingIds, string bearer)
        {
            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/GetAllTrackingDetailsByTrackingIds", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            request.AddBody(trackingIds);

            var response = await client.ExecuteGetAsync(request);

            var list = JsonConvert.DeserializeObject<List<TrackingDetail>>(response.Content);

            return list;
        }

        public async Task<List<ComputedDataForGraph>> GetGraphDataByUserAndDateRangeAsync(int userId, DateTimeOffset dateFrom, DateTimeOffset dateTo, GraphFrequency graphFrequency, bool isForClicks, string bearer, bool isForAttendance = false, bool isForTrackingDetail = false)
        {
            var dto = new GetGraphDataByUserAndDateRange
            {
                UserId = userId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                GraphFrequency = graphFrequency,
                IsForClicks = isForClicks
            };

            var client = new RestClient(ApiAddress);
            var request = new RestRequest("api/tracking/GetAllTrackingDetails", Method.Get);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearer}");
            request.AddParameter("Id", userId);

            var response = await client.ExecuteGetAsync(request);

            var list = JsonConvert.DeserializeObject<List<TrackingDetail>>(response.Content);

            if (isForAttendance)
            {
                return await ProcessDataForGraphs(dto, list, bearer, isForAttendance: true);
            }

            if (isForTrackingDetail)
            {
                return await ProcessDataForGraphs(dto, list, bearer, isForTrackingDetail: true);
            }

            return await ProcessDataForGraphs(dto, list, bearer);
        }

        private async Task<List<ComputedDataForGraph>> ProcessDataForGraphs(GetGraphDataByUserAndDateRange dto, List<TrackingDetail> trackingDetailList, string bearer, bool isForAttendance = false, bool isForTrackingDetail = false)
        {
            var dateFrom = dto.DateFrom;
            var dateTo = dto.DateTo;
            var userId = dto.UserId;
            var isForClicks = dto.IsForClicks;
            var graphFrequency = dto.GraphFrequency;

            List<TrackingDetail> filteredTrackingDetails = new List<TrackingDetail>();

            var trackings = await GetAllTrackingsById(dto.UserId, bearer);

            if (isForAttendance)
            {
                var attendances = new List<ComputedDataForGraph>();

                foreach(var tracking in trackings)
                {
                    attendances.Add(new ComputedDataForGraph
                    {
                        DateFrom = tracking.AttendanceDetail.TimeInDate,
                        DateTo = tracking.AttendanceDetail.TimeOutDate.Value
                    });
                }

                return attendances;
            }

            if (isForTrackingDetail)
            {
                var trackingDetails = new List<ComputedDataForGraph>();

                var result = await GetAllTrackingDetailsByTrackingIds(trackings.Select(a => a.TrackingId).ToList(), bearer);

                foreach (var item in result)
                {
                    // a => a.StartTrackDateTime >= dateFrom.Date && a.EndTrackDateTime.Value.Date <= dateTo.Date
                    if (item.EndTrackDateTime.HasValue)
                    {
                        if (item.StartTrackDateTime >= dateFrom.Date && item.EndTrackDateTime.Value.Date <= dateTo.Date)
                        {
                            trackingDetails.Add(new ComputedDataForGraph()
                            {
                                ProcessName = item.ProcessName,
                                DateFrom = item.StartTrackDateTime,
                                DateTo = item.EndTrackDateTime.Value
                            });
                        }
                    }
                    
                }

                return trackingDetails;
            }

            // trackings = trackings.Where(a => a.AttendanceDetail.TimeOutDate != null).ToList();

            // get the tracking ids for the date range
            var trackingIds = trackings.Where(a => a.AttendanceDetail.TimeInDate >= dateFrom.Date && 
                                                   a.AttendanceDetail.TimeOutDate.Value.Date <= dateTo.Date &&
                                                   a.AppUserId == userId).Select(a => a.TrackingId);

            // trackingDetailList = trackingDetailList.Where(a => a.EndTrackDateTime != null).ToList();

            // get the tracking details of all trackingIds
            foreach (var trackingId in trackingIds)
            {
                var trackingDetails = trackingDetailList.Where(a => 
                                                            a.StartTrackDateTime.Date >= dateFrom.Date && a.EndTrackDateTime.Value.Date <= dateTo.Date &&
                                                            a.TrackingId == trackingId);

                filteredTrackingDetails.AddRange(trackingDetails);
            }

            var listOfValues = new List<ComputedDataForGraph>();

            var years = filteredTrackingDetails.Select(a => a.EndTrackDateTime.Value.Year).Distinct();

            if (isForClicks)
            {
                if (graphFrequency == GraphFrequency.Hourly) // HOURLY
                {
                    foreach (var year in years)
                    {
                        for (int month = 1; month <= 12; month++)
                        {
                            for (int day = 1; day <= 31; day++)
                            {
                                for (int hour = 0; hour <= 23; hour += 2)
                                {
                                    var filteredListByDate = filteredTrackingDetails.Where(a => a.StartTrackDateTime.Year == year &&
                                            a.StartTrackDateTime.Month >= month && a.EndTrackDateTime.Value.Month <= month &&
                                            a.StartTrackDateTime.Day >= day && a.EndTrackDateTime.Value.Day <= day &&
                                            a.StartTrackDateTime.Hour >= hour && a.EndTrackDateTime.Value.Hour <= hour + 1);

                                    if (filteredListByDate.Any())
                                    {
                                        var forGraph = new ComputedDataForGraph();

                                        forGraph.KeyboardClickSum = filteredListByDate.Select(a => a.KeyboardClickCount).Sum();
                                        forGraph.KeyboardClickAverage = filteredListByDate.Select(a => a.KeyboardClickCount).Average();

                                        forGraph.MouseClickSum = filteredListByDate.Select(a => a.MouseClickCount).Sum();
                                        forGraph.MouseClickAverage = filteredListByDate.Select(a => a.MouseClickCount).Average();

                                        forGraph.DateFrom = DateTimeOffset.Parse($"{year}-{month.ToString().PadLeft(2, '0')}-{day.ToString().PadLeft(2, '0')} {hour.ToString().PadLeft(2, '0')}:00");

                                        forGraph.DateTo = DateTimeOffset.Parse($"{year}-{month.ToString().PadLeft(2, '0')}-{day.ToString().PadLeft(2, '0')} {(hour + 1).ToString().PadLeft(2, '0')}:00");

                                        listOfValues.Add(forGraph);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (graphFrequency == GraphFrequency.Daily) // DAILY
                {
                    foreach (var year in years)
                    {
                        for (int month = 1; month <= 12; month++)
                        {
                            for (int day = 1; day <= 31; day++)
                            {
                                var filteredListByDay = filteredTrackingDetails.Where(a => a.StartTrackDateTime.Year == year &&
                                        a.StartTrackDateTime.Month == month && a.EndTrackDateTime.Value.Month == month &&
                                        a.StartTrackDateTime.Day == day && a.EndTrackDateTime.Value.Day == day);

                                if (filteredListByDay.Any())
                                {
                                    var forGraph = new ComputedDataForGraph();

                                    forGraph.KeyboardClickSum = filteredListByDay.Select(a => a.KeyboardClickCount).Sum();
                                    forGraph.KeyboardClickAverage = filteredListByDay.Select(a => a.KeyboardClickCount).Average();

                                    forGraph.MouseClickSum = filteredListByDay.Select(a => a.MouseClickCount).Sum();
                                    forGraph.MouseClickAverage = filteredListByDay.Select(a => a.MouseClickCount).Average();

                                    forGraph.DateFrom = DateTimeOffset.Parse($"{year}-{month.ToString().PadLeft(2, '0')}-{day.ToString().PadLeft(2, '0')}");

                                    forGraph.DateTo = DateTimeOffset.Parse($"{year}-{month.ToString().PadLeft(2, '0')}-{day.ToString().PadLeft(2, '0')}");

                                    listOfValues.Add(forGraph);
                                }
                            }
                        }
                    }
                }
                else if (graphFrequency == GraphFrequency.Monthly) // MONTHLY
                {
                    foreach (var year in years)
                    {
                        for (int month = 1; month <= 12; month++)
                        {
                            var filteredListByMonth = filteredTrackingDetails.Where(a => a.StartTrackDateTime.Year == year &&
                                        a.StartTrackDateTime.Month == month && a.EndTrackDateTime.Value.Month == month);

                            if (filteredListByMonth.Any())
                            {
                                var forGraph = new ComputedDataForGraph();

                                forGraph.KeyboardClickSum = filteredListByMonth.Select(a => a.KeyboardClickCount).Sum();
                                forGraph.KeyboardClickAverage = filteredListByMonth.Select(a => a.KeyboardClickCount).Average();

                                forGraph.MouseClickSum = filteredListByMonth.Select(a => a.MouseClickCount).Sum();
                                forGraph.MouseClickAverage = filteredListByMonth.Select(a => a.MouseClickCount).Average();

                                forGraph.DateFrom = DateTimeOffset.Parse($"{year.ToString().PadLeft(2, '0')}-{month.ToString().PadLeft(2, '0')}-01");

                                forGraph.DateTo = DateTimeOffset.Parse($"{year.ToString().PadLeft(2, '0')}-{month.ToString().PadLeft(2, '0')}-01");

                                listOfValues.Add(forGraph);
                            }
                        }
                    }
                }
            }
            else
            {
                var forGraph = new ComputedDataForGraph();

                forGraph.LifestyleProcessCount = filteredTrackingDetails.Where(a => a.ProcessTypeId == (int)ProcessType.Lifestyle).Select(a => a.ProcessTypeId).Count();
                forGraph.SocialMediaProcessCount = filteredTrackingDetails.Where(a => a.ProcessTypeId == (int)ProcessType.Socials).Select(a => a.ProcessTypeId).Count();
                forGraph.EntertainmentProcessCount = filteredTrackingDetails.Where(a => a.ProcessTypeId == (int)ProcessType.Entertainment).Select(a => a.ProcessTypeId).Count();
                forGraph.ProductivityProcessCount = filteredTrackingDetails.Where(a => a.ProcessTypeId == (int)ProcessType.Productivity).Select(a => a.ProcessTypeId).Count();
                forGraph.OtherProcessCount = filteredTrackingDetails.Where(a => a.ProcessTypeId == (int)ProcessType.Unknown).Select(a => a.ProcessTypeId).Count();

                listOfValues.Add(forGraph);
            }

            return listOfValues;
        }
    }
}
