import 'package:firebase_auth/firebase_auth.dart';
import 'Component.dart';
import 'package:flutter/material.dart';

import 'Toast.dart';
import 'loginscreen.dart';

class loginhomescreen extends StatefulWidget {
  final String name;
  const loginhomescreen({super.key, required this.name});

  @override
  State<loginhomescreen> createState() => _loginhomescreenState();
}

class _loginhomescreenState extends State<loginhomescreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          backgroundColor: bcolor,
          title: Text(" Home Screen "),
          actions: [
            IconButton(
              onPressed: () {
                FirebaseAuth.instance.signOut().then((value) {
                  Navigator.push(context,
                      MaterialPageRoute(builder: (context) => loginscreen()));
                }).onError((error, stackTrace) {
                  toastmessage(error.toString());
                });
              },
              icon: Icon(Icons.logout_outlined),
            )
          ],
        ),
        body: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Center(
              child: Container(
                child: Text(
                  "Welcome to Home Screen",
                  style: TextStyle(
                      fontSize: 20, fontWeight: FontWeight.bold, color: bcolor),
                ),
              ),
            ),
            Text(widget.name),
          ],
        ));
  }
}
