(window.webpackJsonp=window.webpackJsonp||[]).push([[7],{G6fN:function(l,n,u){"use strict";u.r(n);var e,t=u("8Y7J"),o=function(){},i=u("atuK"),r=u("z5nN"),c=u("pMnS"),s=u("pKUh"),a=u("2ZVE"),b=u("SVse"),p=u("s7LF"),d=function(){function l(l,n,u){this.admin=l,this.bsModalService=n,this.alertService=u,this.users=[],this.roles=[{name:"Admin",value:"Admin"},{name:"Moderator",value:"Moderator"},{name:"User",value:"User"}],this.rolesToAdd=[],this.userRoles=[]}return l.prototype.ngOnInit=function(){this.getUsers()},l.prototype.getUsers=function(){var l=this;this.admin.getUsers().subscribe((function(n){l.users=n}))},l.prototype.openModal=function(l,n){var u,e;for(this.user=l,u=0;u<this.roles.length;u++){var t=!1;for(e=0;e<this.user.userRoles.length;e++)if(this.roles[u].name===this.user.userRoles[e].role.name){t=!0,this.roles[u].checked=!0;break}t||(this.roles[u].checked=!1)}this.bsModalRef=this.bsModalService.show(n)},l.prototype.updateRoles=function(){var l=this,n={roles:this.roles.filter((function(l){return!0===l.checked})).map((function(l){return l.name})).slice()};this.admin.updateUserRoles(this.user,n).subscribe((function(){n.roles.forEach((function(n){var u={},e={};e.name=n,u.role=e,l.userRoles.push(u)})),l.user.userRoles=l.userRoles}),(function(n){l.alertService.danger(n)})),this.bsModalRef.hide()},l}(),f=u("AytR"),h=u("IheW"),m=((e=function(){function l(l){this.http=l,this.baseUrl=f.a.baseUrl+"admin/"}return l.prototype.getUsers=function(){return this.http.get(this.baseUrl)},l.prototype.updateUserRoles=function(l,n){return this.http.post(this.baseUrl+l.userName,n)},l}()).ngInjectableDef=t.Pb({factory:function(){return new e(t.Qb(h.c))},token:e,providedIn:"root"}),e),v=u("LqlI"),g=u("3LUQ"),y=t.nb({encapsulation:0,styles:[[""]],data:{}});function k(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,2,null,null,null,null,null,null,null)),(l()(),t.Jb(1,null,["",""])),(l()(),t.pb(2,0,null,null,0,"br",[],null,null,null,null,null))],null,(function(l,n){l(n,1,0,n.context.$implicit.role.name)}))}function R(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,10,"tr",[],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,1,"td",[],null,null,null,null,null)),(l()(),t.Jb(2,null,["",""])),(l()(),t.pb(3,0,null,null,1,"td",[],null,null,null,null,null)),(l()(),t.Jb(4,null,["",""])),(l()(),t.pb(5,0,null,null,2,"td",[],null,null,null,null,null)),(l()(),t.eb(16777216,null,null,1,null,k)),t.ob(7,278528,null,0,b.l,[t.M,t.J,t.q],{ngForOf:[0,"ngForOf"]},null),(l()(),t.pb(8,0,null,null,2,"td",[],null,null,null,null,null)),(l()(),t.pb(9,0,null,null,1,"button",[["class","btn btn-info"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.openModal(l.context.$implicit,t.Bb(l.parent,12))&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Edit Roles"]))],(function(l,n){l(n,7,0,n.context.$implicit.userRoles)}),(function(l,n){l(n,2,0,n.context.$implicit.id),l(n,4,0,n.context.$implicit.userName)}))}function J(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,3,"div",[["class","form-check"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,0,"input",[["class","form-check-input"],["type","checkbox"],["value","role.name"]],[[8,"checked",0],[8,"disabled",0]],[[null,"change"]],(function(l,n,u){var e=!0;return"change"===n&&(e=0!=(l.context.$implicit.checked=!l.context.$implicit.checked)&&e),e}),null,null)),(l()(),t.pb(2,0,null,null,1,"label",[],null,null,null,null,null)),(l()(),t.Jb(3,null,["",""]))],null,(function(l,n){l(n,1,0,n.context.$implicit.checked,"Admin"===n.context.$implicit.name&&"Admin"===n.component.user.userName),l(n,3,0,n.context.$implicit.name)}))}function S(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,5,"div",[["class","modal-header"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,1,"h4",[["class","modal-title pull-left"]],null,null,null,null,null)),(l()(),t.Jb(2,null,["Edit Roles for ",""])),(l()(),t.pb(3,0,null,null,2,"button",[["aria-label","Close"],["class","close pull-right"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.bsModalRef.hide()&&e),e}),null,null)),(l()(),t.pb(4,0,null,null,1,"span",[["aria-hidden","true"]],null,null,null,null,null)),(l()(),t.Jb(-1,null,["\xd7"])),(l()(),t.pb(6,0,null,null,7,"div",[["class","modal-body"]],null,null,null,null,null)),(l()(),t.pb(7,0,null,null,6,"form",[["id","rolesForm"],["novalidate",""]],[[2,"ng-untouched",null],[2,"ng-touched",null],[2,"ng-pristine",null],[2,"ng-dirty",null],[2,"ng-valid",null],[2,"ng-invalid",null],[2,"ng-pending",null]],[[null,"submit"],[null,"reset"]],(function(l,n,u){var e=!0;return"submit"===n&&(e=!1!==t.Bb(l,9).onSubmit(u)&&e),"reset"===n&&(e=!1!==t.Bb(l,9).onReset()&&e),e}),null,null)),t.ob(8,16384,null,0,p.B,[],null,null),t.ob(9,4210688,[["rolesForm",4]],0,p.n,[[8,null],[8,null]],null,null),t.Gb(2048,null,p.b,null,[p.n]),t.ob(11,16384,null,0,p.m,[[4,p.b]],null,null),(l()(),t.eb(16777216,null,null,1,null,J)),t.ob(13,278528,null,0,b.l,[t.M,t.J,t.q],{ngForOf:[0,"ngForOf"]},null),(l()(),t.pb(14,0,null,null,4,"div",[["class","modal-footer"]],null,null,null,null,null)),(l()(),t.pb(15,0,null,null,1,"button",[["class","btn btn-default"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.bsModalRef.hide()&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Cancel"])),(l()(),t.pb(17,0,null,null,1,"button",[["class","btn btn-success"],["form","rolesForm"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.updateRoles()&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Submit"]))],(function(l,n){l(n,13,0,n.component.roles)}),(function(l,n){l(n,2,0,n.component.user.userName),l(n,7,0,t.Bb(n,11).ngClassUntouched,t.Bb(n,11).ngClassTouched,t.Bb(n,11).ngClassPristine,t.Bb(n,11).ngClassDirty,t.Bb(n,11).ngClassValid,t.Bb(n,11).ngClassInvalid,t.Bb(n,11).ngClassPending)}))}function x(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,11,"div",[["class","container"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,10,"table",[["class","table"]],null,null,null,null,null)),(l()(),t.pb(2,0,null,null,7,"tr",[],null,null,null,null,null)),(l()(),t.pb(3,0,null,null,1,"th",[],null,null,null,null,null)),(l()(),t.Jb(-1,null,["User Id"])),(l()(),t.pb(5,0,null,null,1,"th",[],null,null,null,null,null)),(l()(),t.Jb(-1,null,["Username"])),(l()(),t.pb(7,0,null,null,1,"th",[],null,null,null,null,null)),(l()(),t.Jb(-1,null,["Roles"])),(l()(),t.pb(9,0,null,null,0,"th",[],null,null,null,null,null)),(l()(),t.eb(16777216,null,null,1,null,R)),t.ob(11,278528,null,0,b.l,[t.M,t.J,t.q],{ngForOf:[0,"ngForOf"]},null),(l()(),t.eb(0,[["editRoles",2]],null,0,null,S))],(function(l,n){l(n,11,0,n.component.users)}),null)}var C=function(){function l(l,n,u){this.roleTypeService=l,this.bsModalService=n,this.alertService=u}return l.prototype.ngOnInit=function(){var l=this;this.roleTypeService.getRoleTypes().subscribe((function(n){l.roleTypes=n}))},l.prototype.editRoleType=function(l,n){this.title="Edit "+l.roleName,this.role=l,this.bsModalRef=this.bsModalService.show(n)},l.prototype.addRoleType=function(l){this.title="Add",this.role={},this.bsModalRef=this.bsModalService.show(l)},l.prototype.save=function(){var l=this;this.role.id?this.roleTypeService.editRoleType(this.role).subscribe((function(){l.alertService.success("Successfully update role type.")}),(function(n){l.alertService.danger(n)})):this.roleTypeService.createRoleType(this.role).subscribe((function(n){l.roleTypes.push(n),l.alertService.success("Successfully added role type")}),(function(n){l.alertService.danger(n)})),this.bsModalRef.hide()},l.prototype.deleteRole=function(l){var n=this;this.alertService.confirmDelete("Are you sure you want to delete this role? It will delete all the movie roles of this role type?",!0).then((function(u){u.response&&n.roleTypeService.deleteRoleType(l.id).subscribe((function(){n.alertService.success("Successfully deleted the role type."),n.roleTypes.splice(n.roleTypes.indexOf(l),1)}),(function(l){n.alertService.danger(l)}))}))},l}(),B=u("PbSn"),M=t.nb({encapsulation:0,styles:[[""]],data:{}});function z(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,9,"tr",[],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,1,"td",[],null,null,null,null,null)),(l()(),t.Jb(2,null,["",""])),(l()(),t.pb(3,0,null,null,1,"td",[],null,null,null,null,null)),(l()(),t.Jb(4,null,["",""])),(l()(),t.pb(5,0,null,null,4,"td",[["class","btn-group"]],null,null,null,null,null)),(l()(),t.pb(6,0,null,null,1,"button",[["class","btn btn-info"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.editRoleType(l.context.$implicit,t.Bb(l.parent,13))&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Edit"])),(l()(),t.pb(8,0,null,null,1,"button",[["class","btn btn-danger"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.deleteRole(l.context.$implicit)&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Delete"]))],null,(function(l,n){l(n,2,0,n.context.$implicit.id),l(n,4,0,n.context.$implicit.roleName)}))}function T(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,2,"div",[["class","modal-header"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,1,"h4",[["class","modal-title pull-left"]],null,null,null,null,null)),(l()(),t.Jb(2,null,["",""])),(l()(),t.pb(3,0,null,null,8,"div",[["class","modal-body"]],null,null,null,null,null)),(l()(),t.pb(4,0,null,null,1,"label",[["for","roleName"]],null,null,null,null,null)),(l()(),t.Jb(-1,null,["Role Name"])),(l()(),t.pb(6,0,null,null,5,"input",[["class","form-control"],["name","roleName"],["type","text"]],[[2,"ng-untouched",null],[2,"ng-touched",null],[2,"ng-pristine",null],[2,"ng-dirty",null],[2,"ng-valid",null],[2,"ng-invalid",null],[2,"ng-pending",null]],[[null,"ngModelChange"],[null,"input"],[null,"blur"],[null,"compositionstart"],[null,"compositionend"]],(function(l,n,u){var e=!0,o=l.component;return"input"===n&&(e=!1!==t.Bb(l,7)._handleInput(u.target.value)&&e),"blur"===n&&(e=!1!==t.Bb(l,7).onTouched()&&e),"compositionstart"===n&&(e=!1!==t.Bb(l,7)._compositionStart()&&e),"compositionend"===n&&(e=!1!==t.Bb(l,7)._compositionEnd(u.target.value)&&e),"ngModelChange"===n&&(e=!1!==(o.role.roleName=u)&&e),e}),null,null)),t.ob(7,16384,null,0,p.c,[t.B,t.k,[2,p.a]],null,null),t.Gb(1024,null,p.j,(function(l){return[l]}),[p.c]),t.ob(9,671744,null,0,p.o,[[8,null],[8,null],[8,null],[6,p.j]],{name:[0,"name"],model:[1,"model"]},{update:"ngModelChange"}),t.Gb(2048,null,p.k,null,[p.o]),t.ob(11,16384,null,0,p.l,[[4,p.k]],null,null),(l()(),t.pb(12,0,null,null,4,"div",[["class","modal-footer"]],null,null,null,null,null)),(l()(),t.pb(13,0,null,null,1,"button",[["class","btn btn-default"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.bsModalRef.hide()&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Cancel"])),(l()(),t.pb(15,0,null,null,1,"button",[["class","btn btn-success"],["form","rolesForm"],["type","button"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.save()&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Submit"]))],(function(l,n){l(n,9,0,"roleName",n.component.role.roleName)}),(function(l,n){l(n,2,0,n.component.title),l(n,6,0,t.Bb(n,11).ngClassUntouched,t.Bb(n,11).ngClassTouched,t.Bb(n,11).ngClassPristine,t.Bb(n,11).ngClassDirty,t.Bb(n,11).ngClassValid,t.Bb(n,11).ngClassInvalid,t.Bb(n,11).ngClassPending)}))}function U(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,3,"div",[["class","row"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,2,"div",[["class","col-2 mt-2"]],null,null,null,null,null)),(l()(),t.pb(2,0,null,null,1,"button",[["class","btn btn-primary"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==l.component.addRoleType(t.Bb(l,13))&&e),e}),null,null)),(l()(),t.Jb(-1,null,["Add"])),(l()(),t.pb(4,0,null,null,8,"table",[["class","table"]],null,null,null,null,null)),(l()(),t.pb(5,0,null,null,5,"tr",[],null,null,null,null,null)),(l()(),t.pb(6,0,null,null,1,"th",[],null,null,null,null,null)),(l()(),t.Jb(-1,null,["Role ID"])),(l()(),t.pb(8,0,null,null,1,"th",[],null,null,null,null,null)),(l()(),t.Jb(-1,null,["Role Name"])),(l()(),t.pb(10,0,null,null,0,"th",[],null,null,null,null,null)),(l()(),t.eb(16777216,null,null,1,null,z)),t.ob(12,278528,null,0,b.l,[t.M,t.J,t.q],{ngForOf:[0,"ngForOf"]},null),(l()(),t.eb(0,[["roleTypeModal",2]],null,0,null,T))],(function(l,n){l(n,12,0,n.component.roleTypes)}),null)}var $=function(){function l(){}return l.prototype.ngOnInit=function(){},l}(),I=t.nb({encapsulation:0,styles:[[""]],data:{}});function N(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,10,"div",[["class","container"]],null,null,null,null,null)),(l()(),t.pb(1,0,null,null,9,"tabset",[],[[2,"tab-container",null]],null,null,s.b,s.a)),t.ob(2,180224,null,0,a.d,[a.e,t.B,t.k],null,null),(l()(),t.pb(3,0,null,0,3,"tab",[["heading","Users"]],[[1,"id",0],[2,"active",null],[2,"tab-pane",null]],null,null,null,null)),t.ob(4,212992,null,0,a.b,[a.d,t.k,t.B],{heading:[0,"heading"]},null),(l()(),t.pb(5,0,null,null,1,"app-admin-user",[],null,null,null,x,y)),t.ob(6,114688,null,0,d,[m,v.b,g.a],null,null),(l()(),t.pb(7,0,null,0,3,"tab",[["heading","Movie Role Types"]],[[1,"id",0],[2,"active",null],[2,"tab-pane",null]],null,null,null,null)),t.ob(8,212992,null,0,a.b,[a.d,t.k,t.B],{heading:[0,"heading"]},null),(l()(),t.pb(9,0,null,null,1,"app-admin-role-type",[],null,null,null,U,M)),t.ob(10,114688,null,0,C,[B.a,v.b,g.a],null,null)],(function(l,n){l(n,4,0,"Users"),l(n,6,0),l(n,8,0,"Movie Role Types"),l(n,10,0)}),(function(l,n){l(n,1,0,t.Bb(n,2).clazz),l(n,3,0,t.Bb(n,4).id,t.Bb(n,4).active,t.Bb(n,4).addClass),l(n,7,0,t.Bb(n,8).id,t.Bb(n,8).active,t.Bb(n,8).addClass)}))}var O=t.lb("app-admin",$,(function(l){return t.Lb(0,[(l()(),t.pb(0,0,null,null,1,"app-admin",[],null,null,null,N,I)),t.ob(1,114688,null,0,$,[],null,null)],(function(l,n){l(n,1,0)}),null)}),{},{},[]),w=u("Osdn"),A=u("2uy1"),L=u("z/SZ"),F=u("ienR"),P=u("6No5"),j=u("PCNd"),D=u("iInd"),q=u("NJC0"),E={role:["Admin"]};u.d(n,"AdminModuleNgFactory",(function(){return Q}));var Q=t.mb(o,[],(function(l){return t.yb([t.zb(512,t.j,t.X,[[8,[i.a,i.c,i.b,r.a,r.b,c.a,O]],[3,t.j],t.v]),t.zb(4608,b.o,b.n,[t.s,[2,b.C]]),t.zb(4608,w.b,w.b,[]),t.zb(4608,p.y,p.y,[]),t.zb(4608,p.d,p.d,[]),t.zb(4608,A.a,A.a,[t.C,t.z]),t.zb(4608,L.a,L.a,[t.j,t.x,t.p,A.a,t.g]),t.zb(4608,F.r,F.r,[]),t.zb(4608,F.t,F.t,[]),t.zb(4608,F.a,F.a,[]),t.zb(4608,F.h,F.h,[]),t.zb(4608,F.d,F.d,[]),t.zb(4608,F.j,F.j,[]),t.zb(4608,F.s,F.s,[F.t,F.j]),t.zb(4608,v.b,v.b,[t.C,L.a]),t.zb(4608,a.e,a.e,[]),t.zb(4608,P.c,P.c,[]),t.zb(1073742336,b.c,b.c,[]),t.zb(1073742336,F.g,F.g,[]),t.zb(1073742336,v.f,v.f,[]),t.zb(1073742336,a.c,a.c,[]),t.zb(1073742336,P.d,P.d,[]),t.zb(1073742336,w.c,w.c,[]),t.zb(1073742336,p.x,p.x,[]),t.zb(1073742336,p.g,p.g,[]),t.zb(1073742336,p.t,p.t,[]),t.zb(1073742336,j.a,j.a,[]),t.zb(1073742336,D.o,D.o,[[2,D.t],[2,D.k]]),t.zb(1073742336,o,o,[]),t.zb(1024,D.i,(function(){return[[{path:"",component:$,canActivate:[q.a],data:E}]]}),[])])}))},NJC0:function(l,n,u){"use strict";u.d(n,"a",(function(){return r}));var e=u("8Y7J"),t=u("ej43"),o=u("3LUQ"),i=u("iInd"),r=function(){var l=function(){function l(l,n,u){this.authenticationService=l,this.alertService=n,this.router=u}return l.prototype.canActivate=function(l,n){if(this.authenticationService.loggedIn()){var u=l.data.role;if(u){if(this.authenticationService.rolePresent(u))return!0;this.alertService.danger("Access Restricted"),this.router.navigate(["/home"])}return!0}return this.alertService.danger("You need to log in to access this page"),this.router.navigate(["auth/login"],{queryParams:{redirectUrl:n.url}}),!1},l}();return l.ngInjectableDef=e.Pb({factory:function(){return new l(e.Qb(t.a),e.Qb(o.a),e.Qb(i.k))},token:l,providedIn:"root"}),l}()},PbSn:function(l,n,u){"use strict";u.d(n,"a",(function(){return i}));var e=u("AytR"),t=u("8Y7J"),o=u("IheW"),i=function(){var l=function(){function l(l){this.http=l,this.baseUrl=e.a.baseUrl+"roleTypes/"}return l.prototype.getRoleTypes=function(){return this.http.get(this.baseUrl)},l.prototype.createRoleType=function(l){return this.http.post(this.baseUrl,l)},l.prototype.deleteRoleType=function(l){return this.http.delete(this.baseUrl+l)},l.prototype.editRoleType=function(l){return this.http.put(this.baseUrl+l.id,l)},l}();return l.ngInjectableDef=t.Pb({factory:function(){return new l(t.Qb(o.c))},token:l,providedIn:"root"}),l}()},pKUh:function(l,n,u){"use strict";u.d(n,"a",(function(){return i})),u.d(n,"b",(function(){return s}));var e=u("8Y7J"),t=u("2ZVE"),o=u("SVse"),i=e.nb({encapsulation:0,styles:["[_nghost-%COMP%]   .nav-tabs[_ngcontent-%COMP%]   .nav-item.disabled[_ngcontent-%COMP%]   a.disabled[_ngcontent-%COMP%]{cursor:default}"],data:{}});function r(l){return e.Lb(0,[(l()(),e.pb(0,0,null,null,1,"span",[["class","bs-remove-tab"]],null,[[null,"click"]],(function(l,n,u){var e=!0,t=l.component;return"click"===n&&(u.preventDefault(),e=!1!==t.removeTab(l.parent.context.$implicit)&&e),e}),null,null)),(l()(),e.Jb(-1,null,[" \u274c"]))],null,null)}function c(l){return e.Lb(0,[(l()(),e.pb(0,0,null,null,9,"li",[],[[2,"active",null],[2,"disabled",null]],[[null,"keydown"]],(function(l,n,u){var e=!0;return"keydown"===n&&(e=!1!==l.component.keyNavActions(u,l.context.index)&&e),e}),null,null)),e.Gb(512,null,o.x,o.y,[e.q,e.r,e.k,e.B]),e.ob(2,278528,null,0,o.k,[o.x],{ngClass:[0,"ngClass"]},null),e.Cb(3,2),(l()(),e.pb(4,0,null,null,5,"a",[["class","nav-link"],["href","javascript:void(0);"]],[[1,"id",0],[2,"active",null],[2,"disabled",null]],[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=0!=(l.context.$implicit.active=!0)&&e),e}),null,null)),(l()(),e.pb(5,16777216,null,null,2,"span",[],null,null,null,null,null)),e.ob(6,16384,null,0,t.a,[e.M],{ngTransclude:[0,"ngTransclude"]},null),(l()(),e.Jb(7,null,["",""])),(l()(),e.eb(16777216,null,null,1,null,r)),e.ob(9,16384,null,0,o.m,[e.M,e.J],{ngIf:[0,"ngIf"]},null)],(function(l,n){var u=l(n,3,0,"nav-item",n.context.$implicit.customClass||"");l(n,2,0,u),l(n,6,0,n.context.$implicit.headingRef),l(n,9,0,n.context.$implicit.removable)}),(function(l,n){l(n,0,0,n.context.$implicit.active,n.context.$implicit.disabled),l(n,4,0,n.context.$implicit.id?n.context.$implicit.id+"-link":"",n.context.$implicit.active,n.context.$implicit.disabled),l(n,7,0,n.context.$implicit.heading)}))}function s(l){return e.Lb(0,[(l()(),e.pb(0,0,null,null,4,"ul",[["class","nav"]],null,[[null,"click"]],(function(l,n,u){var e=!0;return"click"===n&&(e=!1!==u.preventDefault()&&e),e}),null,null)),e.Gb(512,null,o.x,o.y,[e.q,e.r,e.k,e.B]),e.ob(2,278528,null,0,o.k,[o.x],{klass:[0,"klass"],ngClass:[1,"ngClass"]},null),(l()(),e.eb(16777216,null,null,1,null,c)),e.ob(4,278528,null,0,o.l,[e.M,e.J,e.q],{ngForOf:[0,"ngForOf"]},null),(l()(),e.pb(5,0,null,null,1,"div",[["class","tab-content"]],null,null,null,null,null)),e.Ab(null,0)],(function(l,n){var u=n.component;l(n,2,0,"nav",u.classMap),l(n,4,0,u.tabs)}),null)}}}]);