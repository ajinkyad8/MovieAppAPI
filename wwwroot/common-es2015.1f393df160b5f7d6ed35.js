(window.webpackJsonp=window.webpackJsonp||[]).push([[1],{FXL2:function(e,t,o){"use strict";o.d(t,"a",(function(){return h}));var s=o("AytR"),r=o("XNiG"),i=o("8Y7J"),n=o("IheW");let h=(()=>{class e{constructor(e){this.http=e,this.baseUrl=s.a.baseUrl+"movieRoles/",this.movieRoleSource=new r.a,this.$movieRoleSource=this.movieRoleSource.asObservable()}getMovieRoles(){return this.http.get(this.baseUrl)}getMovieRole(e){return this.http.get(this.baseUrl+e)}createMovieRole(e){return this.http.post(this.baseUrl,e)}deleteMovieRole(e,t){return this.http.post(this.baseUrl+"deleteRequest/"+e,t)}updateMovieRole(e){return this.http.put(this.baseUrl,e)}}return e.ngInjectableDef=i.Pb({factory:function(){return new e(i.Qb(n.c))},token:e,providedIn:"root"}),e})()}}]);