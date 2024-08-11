
var tblAddCourse = document.getElementById('btn-tbl-courses-add');
var crdAddCourse = document.getElementById('btn-card-add-course')
var btnLogout = document.getElementById('btn-logout')
var btnBackAddCourse = document.getElementById('btn-back-add-course');


if (tblAddCourse != null) {
    tblAddCourse.onclick = function () {
        window.location.href = 'AddCourseView';
    };
}
if (crdAddCourse != null) {
    crdAddCourse.onclick = function () {
        window.location.href = 'AddCourseView';
    };
}
if (btnBackAddCourse != null) {
    btnBackAddCourse.onclick = function () {
        window.location.href = 'CoursesView';
    };
}
if (btnLogout != null) {
    btnLogout.onclick = function () {
        window.location.href = 'Logout';
    };
}

