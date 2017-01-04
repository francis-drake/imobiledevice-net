// <copyright file="InstallationProxyApi.cs" company="Quamotion">
// Copyright (c) 2016 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.InstallationProxy
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class InstallationProxyApi : IInstallationProxyApi
    {
        
        /// <summary>
        /// Backing field for the <see cref="Parent"/> property
        /// </summary>
        private ILibiMobileDevice parent;
        
        /// <summary>
        /// Initializes a new instance of the <see cref"InstallationProxyApi"/> class
        /// </summary>
        /// <param name="parent">
        /// The <see cref="ILibiMobileDeviceApi"/> which owns this <see cref="InstallationProxy"/>.
        /// </summary>
        public InstallationProxyApi(ILibiMobileDevice parent)
        {
            this.parent = parent;
        }
        
        /// <inheritdoc/>
        public ILibiMobileDevice Parent
        {
            get
            {
                return this.parent;
            }
        }
        
        /// <summary>
        /// Connects to the installation_proxy service on the specified device.
        /// </summary>
        /// <param name="device">
        /// The device to connect to
        /// </param>
        /// <param name="service">
        /// The service descriptor returned by lockdownd_start_service.
        /// </param>
        /// <param name="client">
        /// Pointer that will be set to a newly allocated
        /// instproxy_client_t upon successful return.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success, or an INSTPROXY_E_* error value
        /// when an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_client_new(iDeviceHandle device, LockdownServiceDescriptorHandle service, out InstallationProxyClientHandle client)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_client_new(device, service, out client);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Starts a new installation_proxy service on the specified device and connects to it.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// instproxy_client_t upon successful return. Must be freed using
        /// instproxy_client_free() after use.
        /// </param>
        /// <param name="label">
        /// The label to use for communication. Usually the program name.
        /// Pass NULL to disable sending the label in requests to lockdownd.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success, or an INSTPROXY_E_* error
        /// code otherwise.
        /// </returns>
        public virtual InstallationProxyError instproxy_client_start_service(iDeviceHandle device, out InstallationProxyClientHandle client, string label)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_client_start_service(device, out client, label);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Disconnects an installation_proxy client from the device and frees up the
        /// installation_proxy client data.
        /// </summary>
        /// <param name="client">
        /// The installation_proxy client to disconnect and free.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success
        /// or INSTPROXY_E_INVALID_ARG if client is NULL.
        /// </returns>
        public virtual InstallationProxyError instproxy_client_free(System.IntPtr client)
        {
            return InstallationProxyNativeMethods.instproxy_client_free(client);
        }
        
        /// <summary>
        /// List installed applications. This function runs synchronously.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid client options include:
        /// "ApplicationType" -> "System"
        /// "ApplicationType" -> "User"
        /// "ApplicationType" -> "Internal"
        /// "ApplicationType" -> "Any"
        /// </param>
        /// <param name="result">
        /// Pointer that will be set to a plist that will hold an array
        /// of PLIST_DICT holding information about the applications found.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_browse(InstallationProxyClientHandle client, PlistHandle clientOptions, out PlistHandle result)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_browse(client, clientOptions, out result);
            result.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// List pages of installed applications in a callback.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid client options include:
        /// "ApplicationType" -> "System"
        /// "ApplicationType" -> "User"
        /// "ApplicationType" -> "Internal"
        /// "ApplicationType" -> "Any"
        /// </param>
        /// <param name="status_cb">
        /// Callback function to process each page of application
        /// information. Passing a callback is required.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_browse_with_callback(InstallationProxyClientHandle client, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_browse_with_callback(client, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Lookup information about specific applications from the device.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="appids">
        /// An array of bundle identifiers that MUST have a terminating
        /// NULL entry or NULL to lookup all.
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Currently there are no known client options, so pass NULL here.
        /// </param>
        /// <param name="result">
        /// Pointer that will be set to a plist containing a PLIST_DICT
        /// holding requested information about the application or NULL on errors.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_lookup(InstallationProxyClientHandle client, System.Collections.ObjectModel.ReadOnlyCollection<string> appids, PlistHandle clientOptions, out PlistHandle result)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_lookup(client, appids, clientOptions, out result);
            result.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Install an application on the device.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="pkg_path">
        /// Path of the installation package (inside the AFC jail)
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid options include:
        /// "iTunesMetadata" -> PLIST_DATA
        /// "ApplicationSINF" -> PLIST_DATA
        /// "PackageType" -> "Developer"
        /// If PackageType -> Developer is specified, then pkg_path points to
        /// an .app directory instead of an install package.
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_install(InstallationProxyClientHandle client, string pkgPath, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_install(client, pkgPath, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Upgrade an application on the device. This function is nearly the same as
        /// instproxy_install; the difference is that the installation progress on the
        /// device is faster if the application is already installed.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="pkg_path">
        /// Path of the installation package (inside the AFC jail)
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid options include:
        /// "iTunesMetadata" -> PLIST_DATA
        /// "ApplicationSINF" -> PLIST_DATA
        /// "PackageType" -> "Developer"
        /// If PackageType -> Developer is specified, then pkg_path points to
        /// an .app directory instead of an install package.
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_upgrade(InstallationProxyClientHandle client, string pkgPath, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_upgrade(client, pkgPath, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Uninstall an application from the device.
        /// </summary>
        /// <param name="client">
        /// The connected installation proxy client
        /// </param>
        /// <param name="appid">
        /// ApplicationIdentifier of the app to uninstall
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Currently there are no known client options, so pass NULL here.
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_uninstall(InstallationProxyClientHandle client, string appid, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_uninstall(client, appid, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// List archived applications. This function runs synchronously.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Currently there are no known client options, so pass NULL here.
        /// </param>
        /// <param name="result">
        /// Pointer that will be set to a plist containing a PLIST_DICT
        /// holding information about the archived applications found.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_lookup_archives(InstallationProxyClientHandle client, PlistHandle clientOptions, out PlistHandle result)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_lookup_archives(client, clientOptions, out result);
            result.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Archive an application on the device.
        /// This function tells the device to make an archive of the specified
        /// application. This results in the device creating a ZIP archive in the
        /// 'ApplicationArchives' directory and uninstalling the application.
        /// </summary>
        /// <param name="client">
        /// The connected installation proxy client
        /// </param>
        /// <param name="appid">
        /// ApplicationIdentifier of the app to archive.
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid options include:
        /// "SkipUninstall" -> Boolean
        /// "ArchiveType" -> "ApplicationOnly"
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_archive(InstallationProxyClientHandle client, string appid, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_archive(client, appid, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Restore a previously archived application on the device.
        /// This function is the counterpart to instproxy_archive.
        /// </summary>
        /// <param name="client">
        /// The connected installation proxy client
        /// </param>
        /// <param name="appid">
        /// ApplicationIdentifier of the app to restore.
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Valid options include:
        /// "ArchiveType" -> "DocumentsOnly"
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_restore(InstallationProxyClientHandle client, string appid, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_restore(client, appid, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Removes a previously archived application from the device.
        /// This function removes the ZIP archive from the 'ApplicationArchives'
        /// directory.
        /// </summary>
        /// <param name="client">
        /// The connected installation proxy client
        /// </param>
        /// <param name="appid">
        /// ApplicationIdentifier of the archived app to remove.
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Currently there are no known client options, so passing NULL is fine.
        /// </param>
        /// <param name="status_cb">
        /// Callback function for progress and status information. If
        /// NULL is passed, this function will run synchronously.
        /// </param>
        /// <param name="user_data">
        /// Callback data passed to status_cb.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        /// <remarks>
        /// If a callback function is given (async mode), this function returns
        /// INSTPROXY_E_SUCCESS immediately if the status updater thread has been
        /// created successfully; any error occuring during the command has to be
        /// handled inside the specified callback function.
        /// </remarks>
        public virtual InstallationProxyError instproxy_remove_archive(InstallationProxyClientHandle client, string appid, PlistHandle clientOptions, InstallationProxyStatusCallBack statusCallBack, System.IntPtr userData)
        {
            return InstallationProxyNativeMethods.instproxy_remove_archive(client, appid, clientOptions, statusCallBack, userData);
        }
        
        /// <summary>
        /// Checks a device for certain capabilities.
        /// </summary>
        /// <param name="client">
        /// The connected installation_proxy client
        /// </param>
        /// <param name="capabilities">
        /// An array of char* with capability names that MUST have a
        /// terminatingÂ NULL entry.
        /// </param>
        /// <param name="client_options">
        /// The client options to use, as PLIST_DICT, or NULL.
        /// Currently there are no known client options, so pass NULL here.
        /// </param>
        /// <param name="result">
        /// Pointer that will be set to a plist containing a PLIST_DICT
        /// holding information if the capabilities matched or NULL on errors.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success or an INSTPROXY_E_* error value if
        /// an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_check_capabilities_match(InstallationProxyClientHandle client, out string capabilities, PlistHandle clientOptions, out PlistHandle result)
        {
            InstallationProxyError returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_check_capabilities_match(client, out capabilities, clientOptions, out result);
            result.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Gets the name from a command dictionary.
        /// </summary>
        /// <param name="command">
        /// The dictionary describing the command.
        /// </param>
        /// <param name="name">
        /// Pointer to store the name of the command.
        /// </param>
        public virtual void instproxy_command_get_name(PlistHandle command, out string name)
        {
            InstallationProxyNativeMethods.instproxy_command_get_name(command, out name);
        }
        
        /// <summary>
        /// Gets the name of a status.
        /// </summary>
        /// <param name="status">
        /// The dictionary status response to use.
        /// </param>
        /// <param name="name">
        /// Pointer to store the name of the status.
        /// </param>
        public virtual void instproxy_status_get_name(PlistHandle status, out string name)
        {
            InstallationProxyNativeMethods.instproxy_status_get_name(status, out name);
        }
        
        /// <summary>
        /// Gets error name, code and description from a response if available.
        /// </summary>
        /// <param name="status">
        /// The dictionary status response to use.
        /// </param>
        /// <param name="name">
        /// Pointer to store the name of an error.
        /// </param>
        /// <param name="description">
        /// Pointer to store error description text if available.
        /// The caller is reponsible for freeing the allocated buffer after use.
        /// If NULL is passed no description will be returned.
        /// </param>
        /// <param name="code">
        /// Pointer to store the returned error code if available.
        /// If NULL is passed no error code will be returned.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS if no error is found or an INSTPROXY_E_* error
        /// value matching the error that áºƒas found in the status.
        /// </returns>
        public virtual InstallationProxyError instproxy_status_get_error(PlistHandle status, out string name, out string description, ref ulong code)
        {
            return InstallationProxyNativeMethods.instproxy_status_get_error(status, out name, out description, ref code);
        }
        
        /// <summary>
        /// Gets total and current item information from a browse response if available.
        /// </summary>
        /// <param name="status">
        /// The dictionary status response to use.
        /// </param>
        /// <param name="total">
        /// Pointer to store the total number of items.
        /// </param>
        /// <param name="current_index">
        /// Pointer to store the current index of all browsed items.
        /// </param>
        /// <param name="current_amount">
        /// Pointer to store the amount of items in the
        /// current list.
        /// </param>
        /// <param name="list">
        /// Pointer to store a newly allocated plist with items.
        /// The caller is reponsible for freeing the list after use.
        /// If NULL is passed no list will be returned. If NULL is returned no
        /// list was found in the status.
        /// </param>
        public virtual void instproxy_status_get_current_list(PlistHandle status, ref ulong total, ref ulong currentIndex, ref ulong currentAmount, out PlistHandle list)
        {
            InstallationProxyNativeMethods.instproxy_status_get_current_list(status, ref total, ref currentIndex, ref currentAmount, out list);
        }
        
        /// <summary>
        /// Gets progress in percentage from a status if available.
        /// </summary>
        /// <param name="status">
        /// The dictionary status response to use.
        /// </param>
        /// <param name="name">
        /// Pointer to store the progress in percent (0-100) or -1 if not
        /// progress was found in the status.
        /// </param>
        public virtual void instproxy_status_get_percent_complete(PlistHandle status, ref int percent)
        {
            InstallationProxyNativeMethods.instproxy_status_get_percent_complete(status, ref percent);
        }
        
        /// <summary>
        /// Creates a new client_options plist.
        /// </summary>
        /// <returns>
        /// A new plist_t of type PLIST_DICT.
        /// </returns>
        public virtual PlistHandle instproxy_client_options_new()
        {
            PlistHandle returnValue;
            returnValue = InstallationProxyNativeMethods.instproxy_client_options_new();
            returnValue.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Adds one or more new key:value pairs to the given client_options.
        /// </summary>
        /// <param name="client_options">
        /// The client options to modify.
        /// </param>
        /// <param name="...">
        /// KEY, VALUE, [KEY, VALUE], NULL
        /// </param>
        /// <remarks>
        /// The keys and values passed are expected to be strings, except for the
        /// keys "ApplicationSINF", "iTunesMetadata", "ReturnAttributes" which are
        /// expecting a plist_t node as value and "SkipUninstall" expects int.
        /// </remarks>
        public virtual void instproxy_client_options_add(PlistHandle clientOptions)
        {
            InstallationProxyNativeMethods.instproxy_client_options_add(clientOptions);
        }
        
        /// <summary>
        /// Adds attributes to the given client_options to filter browse results.
        /// </summary>
        /// <param name="client_options">
        /// The client options to modify.
        /// </param>
        /// <param name="...">
        /// VALUE, VALUE, [VALUE], NULL
        /// </param>
        /// <remarks>
        /// The values passed are expected to be strings.
        /// </remarks>
        public virtual void instproxy_client_options_set_return_attributes(PlistHandle clientOptions)
        {
            InstallationProxyNativeMethods.instproxy_client_options_set_return_attributes(clientOptions);
        }
        
        /// <summary>
        /// Frees client_options plist.
        /// </summary>
        /// <param name="client_options">
        /// The client options plist to free. Does nothing if NULL
        /// is passed.
        /// </param>
        public virtual void instproxy_client_options_free(PlistHandle clientOptions)
        {
            InstallationProxyNativeMethods.instproxy_client_options_free(clientOptions);
        }
        
        /// <summary>
        /// Queries the device for the path of an application.
        /// </summary>
        /// <param name="client">
        /// The connected installation proxy client.
        /// </param>
        /// <param name="appid">
        /// ApplicationIdentifier of app to retrieve the path for.
        /// </param>
        /// <param name="path">
        /// Pointer to store the device path for the application
        /// which is set to NULL if it could not be determined.
        /// </param>
        /// <returns>
        /// INSTPROXY_E_SUCCESS on success, INSTPROXY_E_OP_FAILED if
        /// the path could not be determined or an INSTPROXY_E_* error
        /// value if an error occured.
        /// </returns>
        public virtual InstallationProxyError instproxy_client_get_path_for_bundle_identifier(InstallationProxyClientHandle client, string bundleId, out string path)
        {
            return InstallationProxyNativeMethods.instproxy_client_get_path_for_bundle_identifier(client, bundleId, out path);
        }
    }
}