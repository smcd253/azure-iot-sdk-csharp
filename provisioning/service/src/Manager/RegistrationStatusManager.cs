﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Common;

namespace Microsoft.Azure.Devices.Provisioning.Service
{
    internal static class RegistrationStatusManager
    {
        private const string DeviceRegistrationStatusUriFormat = "registrations/{0}?{1}";

        /// <summary>
        /// Get registration status information.
        /// </summary>
        /// <see cref="ProvisioningServiceClient.GetDeviceRegistrationStateAsync(string)"/>
        ///
        /// <param name="id">the <code>string</code> that identifies the deviceRegistrationState. It cannot be <code>null</code> or empty.</param>
        /// <returns>An <see cref="DeviceRegistrationState"/> with the device registration information.</returns>
        /// <exception cref="ArgumentException">if the provided parameter is not correct.</exception>
        /// <exception cref="ProvisioningServiceClientTransportException">if the SDK failed to send the request to the Device Provisioning Service.</exception>
        /// <exception cref="ProvisioningServiceClientException">if the Device Provisioning Service was not able to execute the get operation.</exception>
        internal static Task<DeviceRegistrationState> GetAsync(
            IContractApiHttp contractApiHttp,
            string id,
            CancellationToken cancellationToken)
        {
            /* SRS_REGISTRATION_STATUS_MANAGER_28_001: [The GetAsync shall throw ArgumentException if the provided ID is null or empty.] */
            ParserUtils.EnsureRegistrationId(id);

            /* SRS_REGISTRATION_STATUS_MANAGER_28_002: [The GetAsync shall sent the Get HTTP request to get the deviceRegistrationState information.] */
            /* SRS_REGISTRATION_STATUS_MANAGER_28_003: [The GetAsync shall return a DeviceRegistrationState object created from the body of the HTTP response.] */
            return contractApiHttp.GetAsync<DeviceRegistrationState>(
                GetDeviceRegistrationStatusUri(id),
                null,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Delete deviceRegistrationState.
        /// </summary>
        /// <see cref="ProvisioningServiceClient.DeleteDeviceRegistrationStatusAsync(string)"/>
        ///
        /// <param name="deviceRegistrationState">is an <see cref="DeviceRegistrationState"/> that describes device registration status which will be deleted. It cannot be <code>null</code>.</param>
        /// <exception cref="ArgumentException">if the provided parameter is not correct.</exception>
        /// <exception cref="ProvisioningServiceClientTransportException">if the SDK failed to send the request to the Device Provisioning Service.</exception>
        /// <exception cref="ProvisioningServiceClientException">if the Device Provisioning Service was not able to execute the delete operation.</exception>
        internal static Task DeleteAsync(
            IContractApiHttp contractApiHttp,
            DeviceRegistrationState deviceRegistrationState,
            CancellationToken cancellationToken)
        {
            /* SRS_REGISTRATION_STATUS_MANAGER_28_004: [The DeleteAsync shall throw ArgumentException if the provided deviceRegistrationState is null.] */
            if (deviceRegistrationState == null)
            {
                throw new ArgumentException("deviceRegistrationState cannot be null.");
            }

            /* SRS_REGISTRATION_STATUS_MANAGER_28_005: [The DeleteAsync shall sent the Delete HTTP request to remove the deviceRegistrationState.] */
            return contractApiHttp.DeleteAsync(
                GetDeviceRegistrationStatusUri(deviceRegistrationState.RegistrationId),
                deviceRegistrationState.ETag,
                null,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Delete deviceRegistrationState.
        /// </summary>
        /// <see cref="ProvisioningServiceClient.DeleteDeviceRegistrationStatusAsync(string)"/>
        /// <see cref="ProvisioningServiceClient.DeleteDeviceRegistrationStatusAsync(string, string)"/>
        ///
        /// <param name="id">is a <code>string</code> with the id of the registrationStatus to delete. It cannot be <code>null</code> or empty.</param>
        /// <param name="eTag">is a <code>string</code> with the eTag of the registrationStatus to delete. It can be <code>null</code> or empty (ignored).</param>
        /// <exception cref="ArgumentException">if the provided registrationId is not correct.</exception>
        /// <exception cref="ProvisioningServiceClientTransportException">if the SDK failed to send the request to the Device Provisioning Service.</exception>
        /// <exception cref="ProvisioningServiceClientException">if the Device Provisioning Service was not able to execute the delete operation.</exception>
        internal static Task DeleteAsync(
            IContractApiHttp contractApiHttp,
            string id,
            CancellationToken cancellationToken,
            string eTag = null)
        {
            /* SRS_REGISTRATION_STATUS_MANAGER_28_006: [The DeleteAsync shall throw ArgumentException if the provided id is null or empty.] */
            ParserUtils.EnsureRegistrationId(id);

            /* SRS_REGISTRATION_STATUS_MANAGER_28_007: [The DeleteAsync shall sent the Delete HTTP request to remove the deviceRegistrationState.] */
            return contractApiHttp.DeleteAsync(
                GetDeviceRegistrationStatusUri(id),
                eTag,
                null,
                null,
                cancellationToken);
        }

        /// <summary>
        /// Create a new deviceRegistrationState query.
        /// </summary>
        /// <see cref="ProvisioningServiceClient.CreateEnrollmentGroupRegistrationStatusQuery(QuerySpecification, string)"/>
        /// <see cref="ProvisioningServiceClient.CreateEnrollmentGroupRegistrationStatusQuery(QuerySpecification, string, int)"/>
        ///
        /// <param name="querySpecification">is a <code>string</code> with the SQL query specification. It cannot be <code>null</code>.</param>
        /// <param name="enrollmentGroupId">is a <code>string</code> with the id which the query run against. It cannot be <code>null</code>.</param>
        /// <param name="pageSize">the <code>int</code> with the maximum number of items per iteration. It can be 0 for default, but not negative.</param>
        /// <returns>A <see cref="Query"/> iterator.</returns>
        /// <exception cref="ArgumentException">if the provided parameter is not correct.</exception>
        internal static Query CreateEnrollmentGroupQuery(QuerySpecification querySpecification, string enrollmentGroupId,  int pageSize = 0)
        {
            //TODO: Implement.

            /* SRS_REGISTRATION_STATUS_MANAGER_28_008: [The CreateQuery shall throw ArgumentException if the provided querySpecification is null.] */
            /* SRS_REGISTRATION_STATUS_MANAGER_28_009: [The CreateQuery shall throw ArgumentException if the provided enrollmentGroupId is not valid.]] */
            /* SRS_REGISTRATION_STATUS_MANAGER_28_010: [The CreateQuery shall return a new Query for DeviceRegistrationState.] */

            throw new NotSupportedException("Query is not supported yet");
        }

        private static Uri GetDeviceRegistrationStatusUri(string id)
        {
            id = WebUtility.UrlEncode(id);
            return new Uri(DeviceRegistrationStatusUriFormat.FormatInvariant(id, SDKUtils.ApiVersionQueryString), UriKind.Relative);
        }
    }
}