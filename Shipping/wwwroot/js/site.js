/*
 * For Toggle Delete button in Users and Roles.
 */
function confirmDelete(UserId, IsDeleteClicked) {
    var DeleteSpan = "DeleteSpan_" + UserId;
    var ConfirmDeleteSpan = "ConfirmDeleteSpan_" + UserId;
    if (IsDeleteClicked) {
        $('#' + DeleteSpan).hide();
        $('#' + ConfirmDeleteSpan).show();
    }
    else {
        $('#' + DeleteSpan).show();
        $('#' + ConfirmDeleteSpan).hide();
    }
}
