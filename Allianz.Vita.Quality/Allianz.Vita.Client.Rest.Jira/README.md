# Rest Methods 

Source: 
https://docs.atlassian.com/software/jira/docs/api/REST/7.6.1/

## Resources

### api/2/
Provide permission information for the current user.
- Get permissions `GET` `/rest/api/2//mypermissions`
- Get all permissions `GET` `/rest/api/2//permissions`

### api/2/application-properties
Get property `GET` `/rest/api/2/application-properties`
Set property via restful table `PUT` `/rest/api/2/application-properties/{id}`
Get advanced settings `GET` `/rest/api/2/application-properties/advanced-settings`

### api/2/applicationrole
Provides REST access to JIRA's Application Roles.
Put bulk `PUT` `/rest/api/2/applicationrole`
Get all `GET` `/rest/api/2/applicationrole`
Get `GET` `/rest/api/2/applicationrole/{key}`
Put `PUT` `/rest/api/2/applicationrole/{key}`

### api/2/attachment
Get attachment `GET` `/rest/api/2/attachment/{id}`
Remove attachment `DELETE` `/rest/api/2/attachment/{id}`
Expand for humans  experimental `GET` `/rest/api/2/attachment/{id}/expand/human`
Expand for machines  experimental `GET` `/rest/api/2/attachment/{id}/expand/raw`
Get attachment meta `GET` `/rest/api/2/attachment/meta`

### api/2/auditing
Resource representing the auditing records
Get records `GET` `/rest/api/2/auditing/record`
Add record `POST` `/rest/api/2/auditing/record`

### api/2/avatar
Get all system avatars `GET` `/rest/api/2/avatar/{type}/system`
Store temporary avatar `POST` `/rest/api/2/avatar/{type}/temporary`
Create avatar from temporary `POST` `/rest/api/2/avatar/{type}/temporaryCrop`

### api/2/cluster/zdu
Approve upgrade `POST` `/rest/api/2/cluster/zdu/approve`
Cancel upgrade `POST` `/rest/api/2/cluster/zdu/cancel`
Acknowledge errors `POST` `/rest/api/2/cluster/zdu/retryUpgrade`
Set ready to upgrade `POST` `/rest/api/2/cluster/zdu/start`
Get state `GET` `/rest/api/2/cluster/zdu/state`

### api/2/comment/{commentId}/properties
Get properties keys  experimental `GET` `/rest/api/2/comment/{commentId}/properties`
Delete property  experimental `DELETE` `/rest/api/2/comment/{commentId}/properties/{propertyKey}`
Set property  experimental `PUT` `/rest/api/2/comment/{commentId}/properties/{propertyKey}`
Get property  experimental `GET` `/rest/api/2/comment/{commentId}/properties/{propertyKey}`

### api/2/component
Create component `POST` `/rest/api/2/component`
Update component `PUT` `/rest/api/2/component/{id}`
Get component `GET` `/rest/api/2/component/{id}`
Delete `DELETE` `/rest/api/2/component/{id}`
Get component related issues `GET` `/rest/api/2/component/{id}/relatedIssueCounts`

### api/2/configuration
Get configuration `GET` `/rest/api/2/configuration`

### api/2/customFieldOption
Get custom field option `GET` `/rest/api/2/customFieldOption/{id}`

### api/2/dashboard
The /dashboard resource.
List `GET` `/rest/api/2/dashboard`
Get dashboard `GET` `/rest/api/2/dashboard/{id}`

### api/2/dashboard/{dashboardId}/items/{itemId}/properties
Get properties keys `GET` `/rest/api/2/dashboard/{dashboardId}/items/{itemId}/properties`
Delete property `DELETE` `/rest/api/2/dashboard/{dashboardId}/items/{itemId}/properties/{propertyKey}`
Set property `PUT` `/rest/api/2/dashboard/{dashboardId}/items/{itemId}/properties/{propertyKey}`
Get property `GET` `/rest/api/2/dashboard/{dashboardId}/items/{itemId}/properties/{propertyKey}`

### api/2/field
Create custom field `POST` `/rest/api/2/field`
Get fields `GET` `/rest/api/2/field`

### api/2/filter
Resource for searches.
Create filter `POST` `/rest/api/2/filter`
Edit filter `PUT` `/rest/api/2/filter/{id}`
Delete filter `DELETE` `/rest/api/2/filter/{id}`
Get filter `GET` `/rest/api/2/filter/{id}`
Default columns `GET` `/rest/api/2/filter/{id}/columns`
Set columns `PUT` `/rest/api/2/filter/{id}/columns`
Reset columns `DELETE` `/rest/api/2/filter/{id}/columns`
Get share permissions `GET` `/rest/api/2/filter/{id}/permission`
Add share permission `POST` `/rest/api/2/filter/{id}/permission`
Get share permission `GET` `/rest/api/2/filter/{id}/permission/{permissionId}`
Delete share permission `DELETE` `/rest/api/2/filter/{id}/permission/{permission-id}`
Get default share scope `GET` `/rest/api/2/filter/defaultShareScope`
Set default share scope `PUT` `/rest/api/2/filter/defaultShareScope`
Get favourite filters `GET` `/rest/api/2/filter/favourite`

### api/2/group
Create group `POST` `/rest/api/2/group`
Get group  deprecated `GET` `/rest/api/2/group`
Remove group `DELETE` `/rest/api/2/group`
Get users from group `GET` `/rest/api/2/group/member`
Add user to group `POST` `/rest/api/2/group/user`
Remove user from group `DELETE` `/rest/api/2/group/user`

### api/2/groups
REST endpoint for searching groups in a group picker
Find groups `GET` `/rest/api/2/groups/picker`

### api/2/groupuserpicker
Find users and groups `GET` `/rest/api/2/groupuserpicker`

### api/2/index/summary
REST resource for index summary
Get index summary  experimental `GET` `/rest/api/2/index/summary`

### api/2/issue
Create issue `POST` `/rest/api/2/issue`
Create issues `POST` `/rest/api/2/issue/bulk`
Get issue `GET` `/rest/api/2/issue/{issueIdOrKey}`
Delete issue `DELETE` `/rest/api/2/issue/{issueIdOrKey}`
Edit issue `PUT` `/rest/api/2/issue/{issueIdOrKey}`
Assign `PUT` `/rest/api/2/issue/{issueIdOrKey}/assignee`
Get comments `GET` `/rest/api/2/issue/{issueIdOrKey}/comment`
Add comment `POST` `/rest/api/2/issue/{issueIdOrKey}/comment`
Update comment `PUT` `/rest/api/2/issue/{issueIdOrKey}/comment/{id}`
Delete comment `DELETE` `/rest/api/2/issue/{issueIdOrKey}/comment/{id}`
Get comment `GET` `/rest/api/2/issue/{issueIdOrKey}/comment/{id}`
Get edit issue meta `GET` `/rest/api/2/issue/{issueIdOrKey}/editmeta`
Notify `POST` `/rest/api/2/issue/{issueIdOrKey}/notify`
Get remote issue links `GET` `/rest/api/2/issue/{issueIdOrKey}/remotelink`
Create or update remote issue link `POST` `/rest/api/2/issue/{issueIdOrKey}/remotelink`
Delete remote issue link by global id `DELETE` `/rest/api/2/issue/{issueIdOrKey}/remotelink`
Get remote issue link by id `GET` `/rest/api/2/issue/{issueIdOrKey}/remotelink/{linkId}`
Update remote issue link `PUT` `/rest/api/2/issue/{issueIdOrKey}/remotelink/{linkId}`
Delete remote issue link by id `DELETE` `/rest/api/2/issue/{issueIdOrKey}/remotelink/{linkId}`
Do transition `POST` `/rest/api/2/issue/{issueIdOrKey}/transitions`
Get transitions `GET` `/rest/api/2/issue/{issueIdOrKey}/transitions`
Remove vote `DELETE` `/rest/api/2/issue/{issueIdOrKey}/votes`
Add vote `POST` `/rest/api/2/issue/{issueIdOrKey}/votes`
Get votes `GET` `/rest/api/2/issue/{issueIdOrKey}/votes`
Get issue watchers `GET` `/rest/api/2/issue/{issueIdOrKey}/watchers`
Add watcher `POST` `/rest/api/2/issue/{issueIdOrKey}/watchers`
Remove watcher `DELETE` `/rest/api/2/issue/{issueIdOrKey}/watchers`
Get issue worklog `GET` `/rest/api/2/issue/{issueIdOrKey}/worklog`
Add worklog `POST` `/rest/api/2/issue/{issueIdOrKey}/worklog`
Get worklog `GET` `/rest/api/2/issue/{issueIdOrKey}/worklog/{id}`
Update worklog `PUT` `/rest/api/2/issue/{issueIdOrKey}/worklog/{id}`
Delete worklog `DELETE` `/rest/api/2/issue/{issueIdOrKey}/worklog/{id}`
Get create issue meta `GET` `/rest/api/2/issue/createmeta`
Get issue picker resource `GET` `/rest/api/2/issue/picker`

### api/2/issue/{issueIdOrKey}/attachments
Issue attachments
Add attachment `POST` `/rest/api/2/issue/{issueIdOrKey}/attachments`

### api/2/issue/{issueIdOrKey}/properties
Get properties keys  experimental `GET` `/rest/api/2/issue/{issueIdOrKey}/properties`
Delete property  experimental `DELETE` `/rest/api/2/issue/{issueIdOrKey}/properties/{propertyKey}`
Set property  experimental `PUT` `/rest/api/2/issue/{issueIdOrKey}/properties/{propertyKey}`
Get property  experimental `GET` `/rest/api/2/issue/{issueIdOrKey}/properties/{propertyKey}`

### api/2/issue/{issueIdOrKey}/subtask
Get sub tasks `GET` `/rest/api/2/issue/{issueIdOrKey}/subtask`
Can move sub task `GET` `/rest/api/2/issue/{issueIdOrKey}/subtask/move`
Move sub tasks `POST` `/rest/api/2/issue/{issueIdOrKey}/subtask/move`

### api/2/issueLink
The Link Issue Resource provides functionality to manage issue links.
Link issues `POST` `/rest/api/2/issueLink`
Get issue link `GET` `/rest/api/2/issueLink/{linkId}`
Delete issue link `DELETE` `/rest/api/2/issueLink/{linkId}`

### api/2/issueLinkType
Rest resource to retrieve a list of issue link types.
Get issue link types `GET` `/rest/api/2/issueLinkType`
Create issue link type `POST` `/rest/api/2/issueLinkType`
Get issue link type `GET` `/rest/api/2/issueLinkType/{issueLinkTypeId}`
Delete issue link type `DELETE` `/rest/api/2/issueLinkType/{issueLinkTypeId}`
Update issue link type `PUT` `/rest/api/2/issueLinkType/{issueLinkTypeId}`

### api/2/issuesecurityschemes
REST resource that allows to view security schemes defined in the product.
Get issue security schemes `GET` `/rest/api/2/issuesecurityschemes`
Get issue security scheme `GET` `/rest/api/2/issuesecurityschemes/{id}`

### api/2/issuetype
Get issue all types `GET` `/rest/api/2/issuetype`
Create issue type `POST` `/rest/api/2/issuetype`
Get issue type `GET` `/rest/api/2/issuetype/{id}`
Delete issue type `DELETE` `/rest/api/2/issuetype/{id}`
Update issue type `PUT` `/rest/api/2/issuetype/{id}`
Get alternative issue types `GET` `/rest/api/2/issuetype/{id}/alternatives`
Create avatar from temporary `POST` `/rest/api/2/issuetype/{id}/avatar`
Store temporary avatar `POST` `/rest/api/2/issuetype/{id}/avatar/temporary`
Store temporary avatar using multi part `POST` `/rest/api/2/issuetype/{id}/avatar/temporary`

### api/2/issuetype/{issueTypeId}/properties
This resource allows to store custom properties for issue types.
Get property keys  experimental `GET` `/rest/api/2/issuetype/{issueTypeId}/properties`
Delete property  experimental `DELETE` `/rest/api/2/issuetype/{issueTypeId}/properties/{propertyKey}`
Set property  experimental `PUT` `/rest/api/2/issuetype/{issueTypeId}/properties/{propertyKey}`
Get property  experimental `GET` `/rest/api/2/issuetype/{issueTypeId}/properties/{propertyKey}`

### api/2/jql/autocompletedata
Resource for auto complete data for searches.
Get auto complete `GET` `/rest/api/2/jql/autocompletedata`
Get field auto complete for query string `GET` `/rest/api/2/jql/autocompletedata/suggestions`

### api/2/licenseValidator
A REST endpoint to provide simple validation services for a JIRA license. Typically used by the setup phase of the JIRA application. This will return an object with a list of errors as key, value pairs.
Show more
Validate `POST` `/rest/api/2/licenseValidator`

### api/2/monitoring/jmx
Are metrics exposed `GET` `/rest/api/2/monitoring/jmx/areMetricsExposed`
Get available metrics `GET` `/rest/api/2/monitoring/jmx/getAvailableMetrics`
Start `GET` `/rest/api/2/monitoring/jmx/startExposing`
Stop `GET` `/rest/api/2/monitoring/jmx/stopExposing`

### api/2/mypreferences
Provide preferences of the currently logged in user.
Get preference `GET` `/rest/api/2/mypreferences`
Remove preference `DELETE` `/rest/api/2/mypreferences`
Set preference `PUT` `/rest/api/2/mypreferences`

### api/2/myself
Currently logged user resource
Update user `PUT` `/rest/api/2/myself`
Get user `GET` `/rest/api/2/myself`
Change my password `PUT` `/rest/api/2/myself/password`

### api/2/notificationscheme
Get notification schemes `GET` `/rest/api/2/notificationscheme`
Get notification scheme `GET` `/rest/api/2/notificationscheme/{id}`

### api/2/password
REST resource for operations related to passwords and the password policy.
Get password policy `GET` `/rest/api/2/password/policy`
Policy check create user `POST` `/rest/api/2/password/policy/createUser`
Policy check update user `POST` `/rest/api/2/password/policy/updateUser`

### api/2/permissionscheme
Resource for managing permission schemes and their attributes.
Get permission schemes `GET` `/rest/api/2/permissionscheme`
Create permission scheme `POST` `/rest/api/2/permissionscheme`
Get scheme attribute  experimental `GET` `/rest/api/2/permissionscheme/{permissionSchemeId}/attribute/{attributeKey}`
Set scheme attribute  experimental `PUT` `/rest/api/2/permissionscheme/{permissionSchemeId}/attribute/{key}`
Get permission scheme `GET` `/rest/api/2/permissionscheme/{schemeId}`
Delete permission scheme `DELETE` `/rest/api/2/permissionscheme/{schemeId}`
Update permission scheme `PUT` `/rest/api/2/permissionscheme/{schemeId}`
Get permission scheme grants `GET` `/rest/api/2/permissionscheme/{schemeId}/permission`
Create permission grant `POST` `/rest/api/2/permissionscheme/{schemeId}/permission`
Delete permission scheme entity `DELETE` `/rest/api/2/permissionscheme/{schemeId}/permission/{permissionId}`
Get permission scheme grant `GET` `/rest/api/2/permissionscheme/{schemeId}/permission/{permissionId}`

### api/2/priority
Get priorities `GET` `/rest/api/2/priority`
Get priority `GET` `/rest/api/2/priority/{id}`

### api/2/project
Get all projects `GET` `/rest/api/2/project`
Create project `POST` `/rest/api/2/project`
Update project `PUT` `/rest/api/2/project/{projectIdOrKey}`
Delete project `DELETE` `/rest/api/2/project/{projectIdOrKey}`
Get project `GET` `/rest/api/2/project/{projectIdOrKey}`
Create avatar from temporary `POST` `/rest/api/2/project/{projectIdOrKey}/avatar`
Update project avatar `PUT` `/rest/api/2/project/{projectIdOrKey}/avatar`
Delete avatar `DELETE` `/rest/api/2/project/{projectIdOrKey}/avatar/{id}`
Store temporary avatar `POST` `/rest/api/2/project/{projectIdOrKey}/avatar/temporary`
Store temporary avatar using multi part `POST` `/rest/api/2/project/{projectIdOrKey}/avatar/temporary`
Get all avatars `GET` `/rest/api/2/project/{projectIdOrKey}/avatars`
Get project components `GET` `/rest/api/2/project/{projectIdOrKey}/components`
Get all statuses `GET` `/rest/api/2/project/{projectIdOrKey}/statuses`
Update project type `PUT` `/rest/api/2/project/{projectIdOrKey}/type/{newProjectTypeKey}`
Get project versions paginated `GET` `/rest/api/2/project/{projectIdOrKey}/version`
Get project versions `GET` `/rest/api/2/project/{projectIdOrKey}/versions`

### api/2/project/{projectIdOrKey}/properties
Get properties keys  experimental `GET` `/rest/api/2/project/{projectIdOrKey}/properties`
Delete property  experimental `DELETE` `/rest/api/2/project/{projectIdOrKey}/properties/{propertyKey}`
Set property  experimental `PUT` `/rest/api/2/project/{projectIdOrKey}/properties/{propertyKey}`
Get property  experimental `GET` `/rest/api/2/project/{projectIdOrKey}/properties/{propertyKey}`

### api/2/project/{projectIdOrKey}/role
Get project roles `GET` `/rest/api/2/project/{projectIdOrKey}/role`
Get project role `GET` `/rest/api/2/project/{projectIdOrKey}/role/{id}`
Set actors `PUT` `/rest/api/2/project/{projectIdOrKey}/role/{id}`
Add actor users `POST` `/rest/api/2/project/{projectIdOrKey}/role/{id}`
Delete actor `DELETE` `/rest/api/2/project/{projectIdOrKey}/role/{id}`

### api/2/project/{projectKeyOrId}/issuesecuritylevelscheme
Resource for associating permission schemes and projects.
Get issue security scheme `GET` `/rest/api/2/project/{projectKeyOrId}/issuesecuritylevelscheme`

### api/2/project/{projectKeyOrId}/notificationscheme
Resource for associating notification schemes and projects.
Get notification scheme `GET` `/rest/api/2/project/{projectKeyOrId}/notificationscheme`

### api/2/project/{projectKeyOrId}/permissionscheme
Resource for associating permission schemes and projects.
Assign permission scheme `PUT` `/rest/api/2/project/{projectKeyOrId}/permissionscheme`
Get assigned permission scheme `GET` `/rest/api/2/project/{projectKeyOrId}/permissionscheme`

### api/2/project/{projectKeyOrId}/securitylevel
Provide security level information of the given project for the current user.
Get security levels for project `GET` `/rest/api/2/project/{projectKeyOrId}/securitylevel`

### api/2/project/type
Get all project types `GET` `/rest/api/2/project/type`
Get project type by key `GET` `/rest/api/2/project/type/{projectTypeKey}`
Get accessible project type by key `GET` `/rest/api/2/project/type/{projectTypeKey}/accessible`

### api/2/projectCategory
Get all project categories `GET` `/rest/api/2/projectCategory`
Create project category `POST` `/rest/api/2/projectCategory`
Get project category by id `GET` `/rest/api/2/projectCategory/{id}`
Remove project category `DELETE` `/rest/api/2/projectCategory/{id}`
Update project category `PUT` `/rest/api/2/projectCategory/{id}`

### api/2/projectvalidate
Get project `GET` `/rest/api/2/projectvalidate/key`

### api/2/reindex
REST resource for starting/stopping/querying indexing.
Reindex `POST` `/rest/api/2/reindex`
Get reindex info `GET` `/rest/api/2/reindex`
Reindex issues `POST` `/rest/api/2/reindex/issue`
Get reindex progress `GET` `/rest/api/2/reindex/progress`

### api/2/reindex/request
REST resource for querying and executing reindex requests.
Process requests `POST` `/rest/api/2/reindex/request`
Get progress `GET` `/rest/api/2/reindex/request/{requestId}`
Get progress bulk `GET` `/rest/api/2/reindex/request/bulk`

### api/2/resolution
Get resolutions `GET` `/rest/api/2/resolution`
Get resolution `GET` `/rest/api/2/resolution/{id}`

### api/2/role
Get project roles `GET` `/rest/api/2/role`
Create project role `POST` `/rest/api/2/role`
Get project roles by id `GET` `/rest/api/2/role/{id}`
Partial update project role `POST` `/rest/api/2/role/{id}`
Fully update project role `PUT` `/rest/api/2/role/{id}`
Delete project role `DELETE` `/rest/api/2/role/{id}`
Get project role actors for role `GET` `/rest/api/2/role/{id}/actors`
Add project role actors to role `POST` `/rest/api/2/role/{id}/actors`
Delete project role actors from role `DELETE` `/rest/api/2/role/{id}/actors`

### api/2/screens
Get fields to add `GET` `/rest/api/2/screens/{screenId}/availableFields`
Get all tabs `GET` `/rest/api/2/screens/{screenId}/tabs`
Add tab `POST` `/rest/api/2/screens/{screenId}/tabs`
Rename tab `PUT` `/rest/api/2/screens/{screenId}/tabs/{tabId}`
Delete tab `DELETE` `/rest/api/2/screens/{screenId}/tabs/{tabId}`
Add field `POST` `/rest/api/2/screens/{screenId}/tabs/{tabId}/fields`
Get all fields `GET` `/rest/api/2/screens/{screenId}/tabs/{tabId}/fields`
Remove field `DELETE` `/rest/api/2/screens/{screenId}/tabs/{tabId}/fields/{id}`
Move field `POST` `/rest/api/2/screens/{screenId}/tabs/{tabId}/fields/{id}/move`
Move tab `POST` `/rest/api/2/screens/{screenId}/tabs/{tabId}/move/{pos}`
Add field to default screen `POST` `/rest/api/2/screens/addToDefault/{fieldId}`

### api/2/search
Resource for searches.
Search using search request `POST` `/rest/api/2/search`
Search `GET` `/rest/api/2/search`

### api/2/securitylevel
Get issuesecuritylevel `GET` `/rest/api/2/securitylevel/{id}`

### api/2/serverInfo
Get server info `GET` `/rest/api/2/serverInfo`

### api/2/settings
REST resource for changing the JIRA system settings
Set base u r l `PUT` `/rest/api/2/settings/baseUrl`
Get issue navigator default columns `GET` `/rest/api/2/settings/columns`
Set issue navigator default columns `PUT` `/rest/api/2/settings/columns`

### api/2/status
Get statuses `GET` `/rest/api/2/status`
Get status `GET` `/rest/api/2/status/{idOrName}`

### api/2/statuscategory
Get status categories `GET` `/rest/api/2/statuscategory`
Get status category `GET` `/rest/api/2/statuscategory/{idOrKey}`

### api/2/universal_avatar
Get avatars `GET` `/rest/api/2/universal_avatar/type/{type}/owner/{owningObjectId}`
Create avatar from temporary `POST` `/rest/api/2/universal_avatar/type/{type}/owner/{owningObjectId}/avatar`
Delete avatar `DELETE` `/rest/api/2/universal_avatar/type/{type}/owner/{owningObjectId}/avatar/{id}`
Store temporary avatar `POST` `/rest/api/2/universal_avatar/type/{type}/owner/{owningObjectId}/temp`
Store temporary avatar using multi part `POST` `/rest/api/2/universal_avatar/type/{type}/owner/{owningObjectId}/temp`

### api/2/upgrade
REST resource for executing and querying delayed upgrades.
Run upgrades now `POST` `/rest/api/2/upgrade`
Get upgrade result `GET` `/rest/api/2/upgrade`

### api/2/user
Update user  experimental `PUT` `/rest/api/2/user`
Create user  experimental `POST` `/rest/api/2/user`
Remove user  experimental `DELETE` `/rest/api/2/user`
Get user `GET` `/rest/api/2/user`
Add user to application  experimental `POST` `/rest/api/2/user/application`
Remove user from application  experimental `DELETE` `/rest/api/2/user/application`
Find bulk assignable users `GET` `/rest/api/2/user/assignable/multiProjectSearch`
Find assignable users `GET` `/rest/api/2/user/assignable/search`
Create avatar from temporary `POST` `/rest/api/2/user/avatar`
Update project avatar `PUT` `/rest/api/2/user/avatar`
Delete avatar `DELETE` `/rest/api/2/user/avatar/{id}`
Store temporary avatar `POST` `/rest/api/2/user/avatar/temporary`
Store temporary avatar using multi part `POST` `/rest/api/2/user/avatar/temporary`
Get all avatars `GET` `/rest/api/2/user/avatars`
Default columns `GET` `/rest/api/2/user/columns`
Set columns `PUT` `/rest/api/2/user/columns`
Reset columns `DELETE` `/rest/api/2/user/columns`
Change user password  experimental `PUT` `/rest/api/2/user/password`
Find users with all permissions `GET` `/rest/api/2/user/permission/search`
Find users for picker `GET` `/rest/api/2/user/picker`
Find users `GET` `/rest/api/2/user/search`
Find users with browse permission `GET` `/rest/api/2/user/viewissue/search`

### api/2/user/properties
Get properties keys `GET` `/rest/api/2/user/properties`
Delete property `DELETE` `/rest/api/2/user/properties/{propertyKey}`
Set property `PUT` `/rest/api/2/user/properties/{propertyKey}`
Get property `GET` `/rest/api/2/user/properties/{propertyKey}`

### api/2/version
Create version `POST` `/rest/api/2/version`
Move version `POST` `/rest/api/2/version/{id}/move`
Get version `GET` `/rest/api/2/version/{id}`
Update version `PUT` `/rest/api/2/version/{id}`
Delete `DELETE` `/rest/api/2/version/{id}`
Merge `PUT` `/rest/api/2/version/{id}/mergeto/{moveIssuesTo}`
Get version related issues `GET` `/rest/api/2/version/{id}/relatedIssueCounts`
Delete `POST` `/rest/api/2/version/{id}/removeAndSwap`
Get version unresolved issues `GET` `/rest/api/2/version/{id}/unresolvedIssueCount`
Get remote version links by version id `GET` `/rest/api/2/version/{versionId}/remotelink`
Create or update remote version link `POST` `/rest/api/2/version/{versionId}/remotelink`
Delete remote version links by version id `DELETE` `/rest/api/2/version/{versionId}/remotelink`
Get remote version link `GET` `/rest/api/2/version/{versionId}/remotelink/{globalId}`
Create or update remote version link `POST` `/rest/api/2/version/{versionId}/remotelink/{globalId}`
Delete remote version link `DELETE` `/rest/api/2/version/{versionId}/remotelink/{globalId}`
Get remote version links `GET` `/rest/api/2/version/remotelink`

### api/2/workflow
REST resource for retrieving workflows.
Get all workflows `GET` `/rest/api/2/workflow`
Delete property `DELETE` `/rest/api/2/workflow/{id}/properties`
Create property `POST` `/rest/api/2/workflow/{id}/properties`
Update property `PUT` `/rest/api/2/workflow/{id}/properties`
Get properties `GET` `/rest/api/2/workflow/{id}/properties`

### api/2/workflowscheme
Create scheme `POST` `/rest/api/2/workflowscheme`
Get by id `GET` `/rest/api/2/workflowscheme/{id}`
Delete scheme `DELETE` `/rest/api/2/workflowscheme/{id}`
Update `PUT` `/rest/api/2/workflowscheme/{id}`
Create draft for parent `POST` `/rest/api/2/workflowscheme/{id}/createdraft`
Delete default `DELETE` `/rest/api/2/workflowscheme/{id}/default`
Update default `PUT` `/rest/api/2/workflowscheme/{id}/default`
Get default `GET` `/rest/api/2/workflowscheme/{id}/default`
Get draft by id `GET` `/rest/api/2/workflowscheme/{id}/draft`
Delete draft by id `DELETE` `/rest/api/2/workflowscheme/{id}/draft`
Update draft `PUT` `/rest/api/2/workflowscheme/{id}/draft`
Get draft default `GET` `/rest/api/2/workflowscheme/{id}/draft/default`
Delete draft default `DELETE` `/rest/api/2/workflowscheme/{id}/draft/default`
Update draft default `PUT` `/rest/api/2/workflowscheme/{id}/draft/default`
Get draft issue type `GET` `/rest/api/2/workflowscheme/{id}/draft/issuetype/{issueType}`
Delete draft issue type `DELETE` `/rest/api/2/workflowscheme/{id}/draft/issuetype/{issueType}`
Set draft issue type `PUT` `/rest/api/2/workflowscheme/{id}/draft/issuetype/{issueType}`
Get draft workflow `GET` `/rest/api/2/workflowscheme/{id}/draft/workflow`
Delete draft workflow mapping `DELETE` `/rest/api/2/workflowscheme/{id}/draft/workflow`
Update draft workflow mapping `PUT` `/rest/api/2/workflowscheme/{id}/draft/workflow`
Get issue type `GET` `/rest/api/2/workflowscheme/{id}/issuetype/{issueType}`
Delete issue type `DELETE` `/rest/api/2/workflowscheme/{id}/issuetype/{issueType}`
Set issue type `PUT` `/rest/api/2/workflowscheme/{id}/issuetype/{issueType}`
Get workflow `GET` `/rest/api/2/workflowscheme/{id}/workflow`
Delete workflow mapping `DELETE` `/rest/api/2/workflowscheme/{id}/workflow`
Update workflow mapping `PUT` `/rest/api/2/workflowscheme/{id}/workflow`

### api/2/worklog
Get ids of worklogs deleted since `GET` `/rest/api/2/worklog/deleted`
Get worklogs for ids `POST` `/rest/api/2/worklog/list`
Get ids of worklogs modified since `GET` `/rest/api/2/worklog/updated`

## Authentication

### auth/1/session
Implement a REST resource for acquiring a session cookie.
Current user `GET` `/rest/auth/1/session`
Logout `DELETE` `/rest/auth/1/session`
Login `POST` `/rest/auth/1/session`

### auth/1/websudo
Release `DELETE` `/rest/auth/1/websudo`