import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Users {
  int userID;
  String loginName;
  String secretKey;
  String password;
  DateTime? lastLogin;
  DateTime? created;
  DateTime? pWLastUpdated;
  String restoreToken;
  String restoreCode;
  DateTime? restoreExpiry;
  bool? selfRegistered;
  bool? disabled;

  Users(
    this.userID,
    this.loginName,
    this.secretKey,
    this.password,
    this.lastLogin,
    this.created,
    this.pWLastUpdated,
    this.restoreToken,
    this.restoreCode,
    this.restoreExpiry,
    this.selfRegistered,
    this.disabled,
  );
  factory Users.login(String _loginName, String _password) {
    return new Users(0, _loginName, '', _password, null, null, null, "", "",
        null, false, false);
  }
  factory Users.fromJson(Map<String, dynamic> json) {
    return new Users(json['userID'], json['loginName'], '', json['password'],
        null, null, null, "", "", null, false, false);
  }

  Map<String, dynamic> toJson() {
    return {
      'userID': userID,
      'loginName': loginName,
      'password': password,
      'secretKey': secretKey,
      /*
    this.loginName,
    this.secretKey,
    this.password,
    this.lastLogin,
    this.created,
    this.pWLastUpdated,
    this.restoreToken,
    this.restoreCode,
    this.restoreExpiry,
    this.selfRegistered,
    this.disabled,
      
       */
    };
  }
}
