﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestWeb.TestApi.Client;
using TestWeb.Tests.Common.Data;

namespace TestWeb.Tests.Common.Builders.Responses
{
    public class ConferenceResponseBuilder
    {
        private readonly ConferenceDetailsResponse _response;

        public ConferenceResponseBuilder(HearingDetailsResponse hearing)
        {
            _response = new ConferenceDetailsResponse()
            {
                Audio_recording_required = hearing.Audio_recording_required,
                Case_name = hearing.Cases.First().Name,
                Case_number = hearing.Cases.First().Number,
                Case_type = hearing.Case_type_name,
                Closed_date_time = null,
                Current_status = ConferenceData.CURRENT_STATUS,
                Endpoints = new List<EndpointResponse>(),
                Hearing_id = hearing.Id,
                Hearing_venue_name = hearing.Hearing_venue_name,
                Id = Guid.NewGuid(),
                Meeting_room = new MeetingRoomResponse(),
                Participants = new List<ParticipantDetailsResponse>(),
                Scheduled_date_time = hearing.Scheduled_date_time,
                Scheduled_duration = hearing.Scheduled_duration,
                Started_date_time = null
            };
        }

        public ConferenceDetailsResponse Build()
        {
            return _response;
        }
    }
}
